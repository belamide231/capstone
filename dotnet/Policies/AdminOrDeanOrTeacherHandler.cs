using System.Security.Claims;
using Amazon.Auth.AccessControlPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class AdminOrDeanOrTeacherRequirement : IAuthorizationRequirement {}
public class AdminOrDeanOrTeacherHandler : AuthorizationHandler<AdminOrDeanRequirement> {

    public const string _Policy = "DeanOrAdminOrTeacher";
    private readonly List<string> _Allowed = new List<string> { "dean", "admin", "teacher" };
    private readonly UserManager<ApplicationUser>? _UserManager;

    public AdminOrDeanOrTeacherHandler(UserManager<ApplicationUser> __UserManager) {
        _UserManager = __UserManager;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrDeanRequirement requirement) {
        
        var Result = context.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier);
        if(Result == null) {
            Console.WriteLine("Result");
            context.Fail();
            return;
        }

        var UserId = Result.Value;
        if(string.IsNullOrEmpty(UserId)) {
            Console.WriteLine("UserId");
            context.Fail();
            return;
        }

        var User = await _UserManager!.FindByIdAsync(UserId);
        if(User == null) {
            Console.WriteLine("UserNull");
            context.Fail();
            return;
        }

        var Roles = User.Roles;
        var Role = Roles.FirstOrDefault();
        if(!_Allowed.Any(e => e == Role)) {
            Console.WriteLine("False");
            context.Fail();
            return;
        }

        context.Succeed(requirement);    
    }
}