using Microsoft.AspNetCore.Identity;

public class PostServices {

    private readonly Mongo _Mongo;
    private readonly Redis _Redis;
    private readonly UserManager<ApplicationUser> _UserManager;

    public PostServices(Mongo __Mongo, Redis __Redis, UserManager<ApplicationUser> __UserManager) {
        _Mongo = __Mongo;
        _Redis = __Redis;
        _UserManager = __UserManager;
    }

    public async Task<Object> GettingPostInDepartmentService() {

        return new {
            Status = StatusCodes.Status200OK,
            Result = (Object)null!
        };
    }

    public async Task<Object> GettingPostInClassService() {

        return new {
            Status = StatusCodes.Status200OK,
            Result = (Object)null!
        };
    }

    public async Task<Object> GettingPostInPortalService() {

        return new {
            Status = StatusCodes.Status200OK,
            Result = (Object)null!
        };
    }

    public async Task<Object> PostingInPortalService() {

        return new {
            Status = StatusCodes.Status202Accepted,
            Result = (Object)null!
        };
    }

    public async Task<Object> PostingInDepartment() {

        return new {
            Status = StatusCodes.Status202Accepted,
            Result = (Object)null!
        };
    }

    public async Task<Object> PostingInClass() {
        
        return new {
            Status = StatusCodes.Status202Accepted,
            Result = (Object)null!
        };
    } 
}