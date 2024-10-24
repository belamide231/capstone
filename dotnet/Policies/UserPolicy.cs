using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class UserPolicy : AuthorizationHandler<TokenHandler> {


    private readonly UserManager<ApplicationUser>? _userManager;
    public UserPolicy(UserManager<ApplicationUser> userManager) => _userManager = userManager;


    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenHandler requirement) {

        var id = context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)?.Value;
        if(string.IsNullOrEmpty(id)) {
            context.Fail();
            return;
        }        
    
        var user = await _userManager!.FindByIdAsync(id);
        
        if(user == null) {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
        return;
    }
}