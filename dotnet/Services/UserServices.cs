using Microsoft.AspNetCore.Identity;


public class UserServices {

    
    private readonly Mongo _mongo;
    private readonly Redis _redis;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserServices(Mongo mongo, Redis redis, UserManager<ApplicationUser> userManager) {
        _mongo = mongo;
        _redis = redis;
        _userManager = userManager;
    }


    public async Task<StatusObject> VerifyEmailAsync(VerifyDTO DTO) {


        var user = await _userManager.FindByEmailAsync(DTO.Email);
        if(user != null)
            return new VerifyResults.EmailAlreadyTaken();


        var codeObjectString = await _redis.VerificationCodes().StringGetAsync(DTO.Email);
        if(!string.IsNullOrEmpty(codeObjectString)) {

            var codeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RegistrationCodeObject>(codeObjectString!);
            if(codeObject!.FailCount >= 3)
                return new VerifyResults.EmailIsLocked();

            return new VerifyResults.VerificationCodeSentAlready(codeObject.Code);
        }


        var verificationCode = new RegistrationCodeObject();
        var mailingResult = await MailHelper.MailRecepient(DTO.Email, verificationCode.Code);


        if(!mailingResult) 
            return new VerifyResults.InternalServerProblem();


        var verificationCodeString = Newtonsoft.Json.JsonConvert.SerializeObject(verificationCode);
        await _redis.VerificationCodes().StringSetAsync(DTO.Email, verificationCodeString, TimeSpan.FromMinutes(5));
        return new VerifyResults.EmailIsValid(verificationCode.Code);
    }


    public async Task<StatusObject> IncrementFailAttemptAsync(IncrementFailAttemptDTO DTO) {


        var verificationCodeString = await _redis.VerificationCodes().StringGetAsync(DTO.Email);
        if(!string.IsNullOrEmpty(verificationCodeString)) {

            var timeRemaining = await _redis.VerificationCodes().KeyTimeToLiveAsync(DTO.Email);
            var verificationCode = Newtonsoft.Json.JsonConvert.DeserializeObject<RegistrationCodeObject>(verificationCodeString!);
            verificationCode!.FailCount++;


            verificationCodeString = Newtonsoft.Json.JsonConvert.SerializeObject(verificationCode);
            await _redis.VerificationCodes().StringSetAsync(DTO.Email, verificationCodeString, timeRemaining);
        }


        return new IncrementResult();
    }


    public async Task<StatusObject> CreateAccountAsync(CreateAccountDTO DTO) {


        var user = new ApplicationUser(DTO.Email!);
        var result = await _userManager.CreateAsync(user, DTO.Password!);


        if(!result.Succeeded)
            return new CreateAccountResult.PasswordConflict(result.Errors.FirstOrDefault()!.Description);


        return new CreateAccountResult.AccountSuccessfullyCreated(DTO.Email!, DTO.Password!);
    }
}