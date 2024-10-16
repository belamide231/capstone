using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;


public class UserServices {

    
    private readonly Mongo _mongo;
    private readonly Redis _redis;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserServices(Mongo mongo, Redis redis, UserManager<ApplicationUser> userManager) {
        _mongo = mongo;
        _redis = redis;
        _userManager = userManager;
    }


    public async Task<StatusObject> VerifyEmailAsync(VerifyEmailDTO DTO) {


        var user = await _userManager.FindByEmailAsync(DTO.Email);
        if(user != null)
            return new VerifyResults.EmailAlreadyTaken();


        var codeObjectString = await _redis.VerificationCodes().StringGetAsync(DTO.Email);
        if(!string.IsNullOrEmpty(codeObjectString)) {

            var codeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationObject>(codeObjectString!);
            if(codeObject!.FailCount >= 3)
                return new VerifyResults.EmailIsLocked();

            if(codeObject.Event == VerificationObject.VerifyingEmailCode)
                return new VerifyResults.VerificationCodeSentAlready(codeObject.Code);
        }


        var verificationCode = new VerificationObject(VerificationObject.VerifyingEmailCode);
        var mailingResult = await MailHelper.MailRecepientAsync(DTO.Email, verificationCode.Code);


        if(!mailingResult) 
            return new VerifyResults.InternalServerProblem();


        var verificationCodeString = Newtonsoft.Json.JsonConvert.SerializeObject(verificationCode);
        await _redis.VerificationCodes().StringSetAsync(DTO.Email, verificationCodeString, TimeSpan.FromMinutes(5));
        return new VerifyResults.EmailIsValid(verificationCode.Code);
    }


    public async Task<StatusObject> UpdateCodeAsync(UpdateCodeDTO DTO) {


        var verificationCodeString = await _redis.VerificationCodes().StringGetAsync(DTO.Email);


        if(string.IsNullOrEmpty(verificationCodeString))
            return new StatusObject(StatusCodes.Status410Gone);


        var verificationCode = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationObject>(verificationCodeString!);


        if(DTO.Match) {


            verificationCode!.Event = VerificationObject.RegisteringAccount;
            verificationCodeString = Newtonsoft.Json.JsonConvert.SerializeObject(verificationCode);

            await _redis.VerificationCodes().StringSetAsync(DTO.Email, verificationCodeString);
            return new StatusObject(StatusCodes.Status202Accepted);


        } else {

            
            if(!string.IsNullOrEmpty(verificationCodeString)) {

                var timeRemaining = await _redis.VerificationCodes().KeyTimeToLiveAsync(DTO.Email);
                verificationCode!.FailCount++;


                verificationCodeString = Newtonsoft.Json.JsonConvert.SerializeObject(verificationCode);
                await _redis.VerificationCodes().StringSetAsync(DTO.Email, verificationCodeString, timeRemaining);
            
            } else
                return new StatusObject(StatusCodes.Status410Gone);

        }


        return new StatusObject(verificationCode!.FailCount >= 3 ? StatusCodes.Status403Forbidden : StatusCodes.Status409Conflict);
    }


    public async Task<StatusObject> CreateAccountAsync(CreateAccountDTO DTO) {


        var verificationObjectString = await _redis.VerificationCodes().StringGetAsync(DTO.Email);
        if(string.IsNullOrEmpty(verificationObjectString))
            return new StatusObject(StatusCodes.Status401Unauthorized);


        var verificationObject = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationObject>(verificationObjectString!);
        if(verificationObject!.Event != VerificationObject.RegisteringAccount)
            return new StatusObject(StatusCodes.Status401Unauthorized); 


        var user = new ApplicationUser(DTO.Email!);
        var result = await _userManager.CreateAsync(user, DTO.Password!);


        if(!result.Succeeded)
            return new CreateAccountResult.PasswordConflict(result.Errors.FirstOrDefault()!.Description);


        await _redis.VerificationCodes().KeyDeleteAsync(DTO.Email);



        if(DTO.Trust) {


            if(string.IsNullOrEmpty(DTO.DeviceId) || string.IsNullOrEmpty(DTO.DeviceIdIdentifier)) {

                DTO.DeviceId = Guid.NewGuid() + "-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + "-" + Guid.NewGuid();
                DTO.DeviceIdIdentifier = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            }
                
            
            var HashedDeviceId = BCryptHelper.Hash(DTO.DeviceId);
            var DeviceInfoForDb = new DeviceIdSchema(DTO.DeviceIdIdentifier, HashedDeviceId);
            await _mongo.ApplicationUsers().FindOneAndUpdateAsync(
                Builders<ApplicationUser>.Filter.Eq(f => f.Id, user.Id),
                Builders<ApplicationUser>.Update.Push(f => f.DeviceIds, DeviceInfoForDb) 
            );

            
            var DeviceInfoForResponse = new DeviceIdSchema(DTO.DeviceIdIdentifier, DTO.DeviceId);
            return new CreateAccountResult.AccountSuccessfullyCreatedTrust(DTO.Email!, DTO.Password!, DeviceInfoForResponse);
        }


        return new CreateAccountResult.AccountSuccessfullyCreated(DTO.Email!, DTO.Password!);
    }


    public async Task<StatusObject> VerifyCredentialAsync(VerifyCredentialDTO DTO) {


        var user = await _userManager.FindByNameAsync(DTO.Username);
        if(user == null) 
            return new StatusObject(StatusCodes.Status401Unauthorized);


        var existingVerificationString = await _redis.VerificationCodes().StringGetAsync(user.Email);
        if(!string.IsNullOrEmpty(existingVerificationString)) {

            var existingVerificationObject = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationObject>(existingVerificationString!);
            if(existingVerificationObject!.FailCount >= 3) 
                return new StatusObject(StatusCodes.Status423Locked);
        }


        var locked = await _userManager.IsLockedOutAsync(user);
        if(locked) 
            return new StatusObject(StatusCodes.Status423Locked);


        var match = await _userManager.CheckPasswordAsync(user, DTO.Password);
        if(!match) {
            await _userManager.AccessFailedAsync(user);
            return new StatusObject(StatusCodes.Status403Forbidden);
        }


        await _userManager.ResetAccessFailedCountAsync(user);
        var deviceInfo = user.DeviceIds.FirstOrDefault(f => f.DeviceIdIdentifier == DTO.DeviceIdIdentifier);
        if(deviceInfo != null) {

            if((!string.IsNullOrEmpty(DTO.DeviceId) || !string.IsNullOrEmpty(DTO.DeviceIdIdentifier)) && BCryptHelper.Verify(DTO.DeviceId, deviceInfo.DeviceId)) {

                var token = new JwtHelper(user.Roles);
                return new CredentialVerificationResult(token.ToString(), StatusCodes.Status200OK);                                 
            }
        }


        var verificationObject = new VerificationObject(VerificationObject.VerifyingCredential);
        var verificationObjectSerialize = Newtonsoft.Json.JsonConvert.SerializeObject(verificationObject);


        var emailResult = await MailHelper.MailRecepientAsync(user.Email!, verificationObject.Code);
        if(!emailResult)
            return new StatusObject(StatusCodes.Status500InternalServerError);


        var storingResult = await _redis.VerificationCodes().StringSetAsync(user.Email, verificationObjectSerialize, TimeSpan.FromMicroseconds(5));
        if(!storingResult) 
            return new StatusObject(StatusCodes.Status500InternalServerError);


        return new StatusObject(StatusCodes.Status202Accepted);
    }


    public async Task<StatusObject> VerifyLoginCodeAsync(VerifyLoginCodeDTO DTO) {

        
        var user = await _userManager.FindByNameAsync(DTO.Username);
        if(user == null) 
            return new StatusObject(StatusCodes.Status401Unauthorized);


        var verificationObjectSerialize = await _redis.VerificationCodes().StringGetAsync(user!.Email);
        if(string.IsNullOrEmpty(verificationObjectSerialize))
            return new StatusObject(StatusCodes.Status419AuthenticationTimeout);


        var verificationObject = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationObject>(verificationObjectSerialize!);
        if(verificationObject!.FailCount >= 3) 
            return new StatusObject(StatusCodes.Status423Locked);


        if(verificationObject.Code != DTO.Code) {

            var timeRemaining = await _redis.VerificationCodes().KeyTimeToLiveAsync(user.Email);
            verificationObject.FailCount++;


            var serializedVerificationObject = Newtonsoft.Json.JsonConvert.SerializeObject(verificationObject); 
            await _redis.VerificationCodes().StringSetAsync(user.Email, serializedVerificationObject, timeRemaining);

            return new StatusObject(StatusCodes.Status409Conflict);
        }


        if(DTO.Trust && (string.IsNullOrEmpty(DTO.DeviceIdIdentifier) || string.IsNullOrEmpty(DTO.DeviceId))) {

            DTO.DeviceIdIdentifier = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            DTO.DeviceId = Guid.NewGuid() + "-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + "-" + Guid.NewGuid();
        }


        var deviceInfo = new DeviceIdSchema(DTO.DeviceIdIdentifier, DTO.DeviceId);
        var result = await _mongo.ApplicationUsers().FindOneAndUpdateAsync(
            Builders<ApplicationUser>.Filter.Eq(f => f.UserName, user.UserName),
            Builders<ApplicationUser>.Update.Push(f => f.DeviceIds, deviceInfo)
        );


        if(result == null && !result!.DeviceIds.Contains(deviceInfo))


        var token = new JwtHelper(user.Roles);
        return new CredentialVerificationResult(token.ToString(), StatusCodes.Status200OK);   
    }
}