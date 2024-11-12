using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class UserRequirement : IAuthorizationRequirement {}

public class UserHandler : AuthorizationHandler<UserRequirement> {


    private readonly UserManager<ApplicationUser>? _userManager;
    public const string _Policy = "user";
    public UserHandler(UserManager<ApplicationUser> userManager) => _userManager = userManager;


    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRequirement requirement) {

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