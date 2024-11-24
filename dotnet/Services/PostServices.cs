using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

public class PostServices {

    private readonly Mongo _Mongo;
    private readonly Redis _Redis;
    private readonly UserManager<ApplicationUser> _UserManager;

    public PostServices(Mongo __Mongo, Redis __Redis, UserManager<ApplicationUser> __UserManager) {
        _Mongo = __Mongo;
        _Redis = __Redis;
        _UserManager = __UserManager;
    }

    public async Task<Object> PostingInHome(PostInHomeDTO DTO, string UserId) {

        var user = await _UserManager.FindByIdAsync(UserId);
        var type = "";

        if(DTO.Description!.Contains("#Announcement")) {
            type = "Announcement";
        } else if(DTO.Description.Contains("#Question")) {
            type = "Question";
        } else {
            type = "Default";
        }

        var Post = new PostSchema(DTO.Description, type, UserId, "home");
        
        if(user!.Roles.FirstOrDefault() == "student") {

            var RequestPost = new PendingPostSchema(Post, UserId, type, DTO.In!);
            
            try {

                await _Mongo.PendingPostCollection().InsertOneAsync(RequestPost);
                Post.PostedBy = user.Email;
                RequestPost.RequestedBy = user.Email;

                return new {
                    Status = StatusCodes.Status201Created,
                    Data = RequestPost
                };

            } catch {

                return new {
                    Status = StatusCodes.Status500InternalServerError,
                    Data = (Object)null!
                };
            }

        } else {

            try {

                await _Mongo.PostCollection().InsertOneAsync(Post);
                Post.PostedBy = user.Email;

                return new {
                    Status = StatusCodes.Status202Accepted,
                    Data = Post
                };

            } catch {

                return new {
                    Status = 500,
                    Data = (Object)null!
                };
            }
        }
    }

    public async Task<object> GetHomePosts() {

        try {

            var Result = await _Mongo.PostCollection().Find(
                Builders<PostSchema>.Filter.Eq(F => F.In, "home")
            ).ToListAsync();


            var UpdatedResult = await Task.WhenAll(Result.Select(async(obj) => {
                obj.PostedBy = (await _UserManager.FindByIdAsync(obj.PostedBy!))!.Email;
                return obj;
            }));

            return new {
                Status = StatusCodes.Status200OK,
                Data = Result
            };

        } catch {

            return new {
                Status = StatusCodes.Status500InternalServerError,
                Data = (Object)null!
            };
        }
    }

    public async Task<object> GetHomePendingPostService() {

        try {

            var Result = await _Mongo.PendingPostCollection().Find(
                Builders<PendingPostSchema>.Filter.Empty
            ).ToListAsync();

            return new {
                Status = StatusCodes.Status200OK,   
                Data = Result
            }; 

        } catch {

            return new {
                Status = StatusCodes.Status500InternalServerError,
                Data = (Object)null!
            };
        }
    }

    // public async Task<Object> GettingPostInClassService() {

    //     return new {
    //         Status = StatusCodes.Status200OK,
    //         Result = (Object)null!
    //     };
    // }

    // public async Task<Object> GettingPostInPortalService() {

    //     return new {
    //         Status = StatusCodes.Status200OK,
    //         Result = (Object)null!
    //     };
    // }

    // public async Task<Object> PostingInPortalService() {

    //     return new {
    //         Status = StatusCodes.Status202Accepted,
    //         Result = (Object)null!
    //     };
    // }

    // public async Task<Object> PostingInDepartment() {

    //     return new {
    //         Status = StatusCodes.Status202Accepted,
    //         Result = (Object)null!
    //     };
    // }

    // public async Task<Object> PostingInClass() {
        
    //     return new {
    //         Status = StatusCodes.Status202Accepted,
    //         Result = (Object)null!
    //     };
    // } 

    public async Task<Object> GetStudentsPendingPostInHome(string UserId) {

        try {

            var result = await _Mongo.PendingPostCollection().Find(
                Builders<PendingPostSchema>.Filter.Eq(F => F.RequestedBy, UserId)
            ).ToListAsync();

            return new {
                Status = StatusCodes.Status200OK,
                Data = result
            };

        } catch {

            return new {
                Status = StatusCodes.Status500InternalServerError,
                Data = (Object)null!
            };

        }
    }

    public async Task<int> CancelStudentPendingPostService(CancelStudentPendingPostDTO DTO) {

        try {

            await _Mongo.PendingPostCollection().DeleteOneAsync(
                Builders<PendingPostSchema>.Filter.Eq(F => F.Id, DTO.StudentsPendingPostId)
            );

            return StatusCodes.Status200OK;

        } catch {

            return StatusCodes.Status500InternalServerError;
        }
    }

    public async Task<Object> GetAllRequestPostInHomeService() {

        try {

            var Result = await _Mongo.PendingPostCollection().Find(
                Builders<PendingPostSchema>.Filter.Eq(f => f.In, "Home")
            ).ToListAsync();

            var UpdatedResult = await Task.WhenAll(Result.Select(async(obj) => {
                obj.Post!.PostedBy = (await _UserManager.FindByIdAsync(obj.Post!.PostedBy!))!.Email;
                return obj;
            }));

            return new {
                Status = StatusCodes.Status200OK,
                Data = UpdatedResult
            };
            
        } catch {

            return new {
                Status = StatusCodes.Status500InternalServerError,
                Data = (Object)null! 
            };
        }
    }

    public async Task<int> ApprovePendingPostInHome(CancelStudentPendingPostDTO DTO) {

        try {

            var Post = await _Mongo.PendingPostCollection().Find(
                Builders<PendingPostSchema>.Filter.Eq(f => f.Id, DTO.StudentsPendingPostId)
            ).ToListAsync();

            await _Mongo.PostCollection().InsertOneAsync(Post.FirstOrDefault()!.Post!);

            return StatusCodes.Status201Created;

        } catch {

            return StatusCodes.Status500InternalServerError;
        }
    }
}