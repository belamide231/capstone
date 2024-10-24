using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PolicyController : ControllerBase {

    public const string _UserPolicy = "user";


    [Authorize(_UserPolicy)]
    [HttpPost("user")]
    public IActionResult UserPolicy() {
        return Ok();
    }
}
