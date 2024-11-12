using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class DeanRequirement : IAuthorizationRequirement {}
public class DeanHandler : AuthorizationHandler<DeanRequirement> {

    public const string _Policy = "dean";
    private readonly UserManager<ApplicationUser>? _UserManager;

    public DeanHandler(UserManager<ApplicationUser> __UserManager) {
        _UserManager = __UserManager;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeanRequirement requirement) {

        var Result = context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier);
        if(Result == null) {
            Console.WriteLine("RESULT");
            context.Fail();
            return;
        }

        var UserId = Result.Value;
        if(string.IsNullOrEmpty(UserId)) {
            Console.WriteLine("USERID");
            context.Fail();
            return;
        }

        var User = await _UserManager!.FindByIdAsync(UserId);
        if(User == null) {
            Console.WriteLine("USER");
            context.Fail();
            return;
        }

        var Roles = User.Roles;
        var Role = Roles.FirstOrDefault();
        if(Role != _Policy) {
            Console.WriteLine("ROLE");
            context.Fail();
            return;
        }

        context.Succeed(requirement);    
    }
}