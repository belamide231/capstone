using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class TeacherRequirement : IAuthorizationRequirement {}

public class TeacherHandler : AuthorizationHandler<TeacherRequirement> {

    public const string _Policy = "teacher";
    private readonly UserManager<ApplicationUser> _UserManager;

    public TeacherHandler(UserManager<ApplicationUser> __UserManager) {
        _UserManager = __UserManager;
    }

    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, TeacherRequirement requirement) {

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
        if(Role != _Policy) {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}