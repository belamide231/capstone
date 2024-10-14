using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]/register")]
[ApiController]
public class UserController : ControllerBase {
    

    private readonly UserServices _services;
    public UserController(UserServices services) => _services = services;

    
    [HttpPost("verifyEmail")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyDTO DTO) {
        var result = await _services.VerifyEmailAsync(DTO);
        return StatusCode(result.Status, result);
    }


    [HttpPost("incrementFailAttempt")]
    public async Task<IActionResult> IncrementFailAttempt([FromBody] IncrementFailAttemptDTO DTO) {
        var result = await _services.IncrementFailAttemptAsync(DTO);
        return StatusCode(result.Status, result);
    }


    [HttpPost("createAccount")]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO DTO) {
        var result = await _services.CreateAccountAsync(DTO);
        return StatusCode(result.Status, result);
    }
}
