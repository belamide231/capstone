using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
    

    private readonly UserServices _services;
    public UserController(UserServices services) => _services = services;

    [HttpPost("register/verifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDTO DTO) {
        var result = await _services.VerifyEmailAsync(DTO);
        return StatusCode(result.Status, result);
    }
    
    [HttpPost("register/updateCode")]
    public async Task<IActionResult> UpdateCode([FromBody] UpdateCodeDTO DTO) {
        var result = await _services.UpdateCodeAsync(DTO);
        return StatusCode(result.Status, result);
    }

    [HttpPost("register/createAccount")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO DTO) {
        var result = await _services.CreateAccountAsync(DTO);
        return StatusCode(result.Status, result);
    }

    [HttpPost("login/verifyCredential")]
    public async Task<IActionResult> VerifyCredential([FromBody] VerifyCredentialDTO DTO) {
        var result = await _services.VerifyCredentialAsync(DTO);
        return StatusCode(result.Status, result);
    } 

    [HttpPost("login/verifyLoginCode")]
    public async Task<IActionResult> VerifyLoginCode([FromBody] VerifyLoginCodeDTO DTO) {
        var result = await _services.VerifyLoginCodeAsync(DTO);
        return StatusCode(result.Status, result);
    }

    [HttpPost("recover/verifySyncedEmail")]
    public async Task<IActionResult> VerifySyncedEmail([FromBody] VerifyEmailRecoveryDTO DTO) {
        var result = await _services.VerifyEmailForRecoveryAsync(DTO);
        return StatusCode(result.Status, result);
    }

    [HttpPost("recover/verifyCode")]
    public async Task<IActionResult> VerifyRecoveryCode([FromBody] VerifyRecoveryCodeDTO DTO) {
        var result = await _services.VerifyRecoveryCodeAsync(DTO);
        return StatusCode(result.Status, result);
    }   

    [HttpPost("recover/newPassword")]
    public async Task<IActionResult> NewPassword([FromBody] NewPasswordRecoveryDTO DTO) {
        var result = await _services.NewPasswordRecoveryAsync(DTO);
        return StatusCode(result.Status, result);
    }
}