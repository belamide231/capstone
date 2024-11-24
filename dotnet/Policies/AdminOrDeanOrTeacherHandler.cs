using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class AdminOrDeanOrTeacherRequirement : IAuthorizationRequirement {}
public class AdminOrDeanOrTeacherHandler : AuthorizationHandler<AdminOrDeanOrTeacherRequirement> {

    public const string _Policy = "DeanOrAdminOrTeacher";
    private readonly List<string> _Allowed = new List<string> { "dean", "admin", "teacher" };
    private readonly UserManager<ApplicationUser>? _UserManager;

    public AdminOrDeanOrTeacherHandler(UserManager<ApplicationUser> __UserManager) {
        _UserManager = __UserManager;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrDeanOrTeacherRequirement requirement) {
        
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