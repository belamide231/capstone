using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]/register")]
[ApiController]
public class UserController : ControllerBase {
    

    private readonly UserServices _services;
    public UserController(UserServices services) => _services = services;

    
    // api/user/register/verifyEmail
    [HttpPost("verifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyDTO DTO) {
        var result = await _services.VerifyEmailAsync(DTO);
        return StatusCode(result.Status, result);
    }

    
    // api/user/register/updateCode
    [HttpPost("updateCode")]
    public async Task<IActionResult> UpdateCode([FromBody] UpdateCodeDTO DTO) {
        var result = await _services.UpdateCodeAsync(DTO);
        return StatusCode(result.Status, result);
    }


    // api/user/register/createAccount
    [HttpPost("createAccount")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO DTO) {
        var result = await _services.CreateAccountAsync(DTO);
        return StatusCode(result.Status, result);
    }
}
