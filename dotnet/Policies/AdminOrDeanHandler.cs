using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class AdminOrDeanRequirement : IAuthorizationRequirement {}
public class AdminOrDeanHandler : AuthorizationHandler<AdminOrDeanRequirement> {

    public const string _Policy = "DeanOrAdmin";
    private readonly List<string> _Allowed = new List<string> { "dean", "admin" };
    private readonly UserManager<ApplicationUser>? _UserManager;

    public AdminOrDeanHandler(UserManager<ApplicationUser> __UserManager) {
        _UserManager = __UserManager;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrDeanRequirement requirement) {
        
        var Result = context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier);
        if(Result == null) {
            context.Fail();
            return;
        }

        var UserId = Result.Value;
        if(string.IsNullOrEmpty(UserId)) {
            context.Fail();
            return;
        }

        var User = await _UserManager!.FindByIdAsync(UserId);
        if(User == null) {
            context.Fail();
            return;
        }

        var Roles = User.Roles;
        var Role = Roles.FirstOrDefault();
        if(!_Allowed.Any(e => e == Role)) {
            context.Fail();
            return;
        }

        context.Succeed(requirement);    
    }
}