using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PolicyController : ControllerBase {

    private UserManager<ApplicationUser>? _userManager;
    public PolicyController(UserManager<ApplicationUser> userManager) => _userManager = userManager;


    [Authorize(UserHandler._Policy)]
    [HttpPost("user")]
    public async Task<IActionResult> UserPolicyControl() {
        
        var id = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)!.Value;
        var user = await _userManager!.FindByIdAsync(id);
        var role = user!.Roles.FirstOrDefault();
        
        return Ok(new { role, email = user.Email, id = user.Id.ToString() });
    }
}
