using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using MongoDB.Bson;
using MongoDB.Driver;

public class DepartmentServices {

    private Mongo _Mongo;
    private Redis _Redis;
    private UserManager<ApplicationUser> _UserManager;

    public DepartmentServices(Mongo __Mongo, Redis __Redis, UserManager<ApplicationUser> __UserManager) {
        _Mongo = __Mongo;
        _Redis = __Redis;
        _UserManager = __UserManager;
    }

    public async Task<Object> CreateDepartmentService(CreatingDepartmentDTO DTO, string UserId) {

        var User = await _UserManager.FindByIdAsync(UserId);
        var Roles = User!.Roles;
        var UserRole = Roles.FirstOrDefault();
        var UserEmail = User.Email;

        var NewDepartment = new DepartmentSchema(DTO.DepartmentName!, DTO.DepartmentDescription!, UserId);
        
        if(UserRole == "admin") {

            try {

                await _Mongo.DepartmentCollection().InsertOneAsync(NewDepartment);
                return new {
                    Status = StatusCodes.Status201Created
                };

            } catch {

                return new {
                    Status = StatusCodes.Status500InternalServerError
                };
            }

        } else {

            var PendingNewDepartment = new PendingDepartmentSchema(NewDepartment, UserEmail!);
            
            try {

                await _Mongo.PendingDepartmentsCollection().InsertOneAsync(PendingNewDepartment);
                return StatusCodes.Status202Accepted;

            } catch {

                return StatusCodes.Status500InternalServerError;
            }
        }
    }

    public async Task<Object> GetDepartmentRequestService(string UserId) {
        
        try {

            var result = await _Mongo.PendingDepartmentsCollection().Find(
                Builders<PendingDepartmentSchema>.Filter.Empty
            ).ToListAsync();

            return new {
                Status = StatusCodes.Status200OK,
                Result = result
            };

        } catch {

            return new {
                Status = StatusCodes.Status500InternalServerError,
                Result = (Object?)null
            };
        }
    }

    public async Task<Object> GetDepartmentsService() {

        try {

            var result = await _Mongo.DepartmentCollection().Find(
                Builders<DepartmentSchema>.Filter.Empty
            ).ToListAsync();

            return new {
                Status = StatusCodes.Status200OK,
                Result = result
            };

        } catch {

            return new {
                Status = StatusCodes.Status500InternalServerError,
                Result = (Object?)null
            };
        }
    }

    public async Task<Object> GetDepartmentService(string DepartmentName, string UserId) {

        var role = (await _UserManager.FindByIdAsync(UserId))!.Roles.FirstOrDefault();

        if(role == "admin") {

            try {

                var result = await _Mongo.DepartmentCollection().Find(
                    Builders<DepartmentSchema>.Filter.Eq(f => f.DepartmentName, DepartmentName)
                ).ToListAsync();

                return new {
                    Status = StatusCodes.Status200OK,
                    Result = result
                };

            } catch {

                return new {
                    Status = StatusCodes.Status500InternalServerError,
                    Result = (Object?)null
                };
            }

        } else {

            try {

                var result = await _Mongo.DepartmentCollection().Find(
                    Builders<DepartmentSchema>.Filter.Or(
                        Builders<DepartmentSchema>.Filter.And(
                            Builders<DepartmentSchema>.Filter.Eq(f => f.DepartmentName, DepartmentName),
                            Builders<DepartmentSchema>.Filter.In("MembersId", new [] { UserId })
                        ),
                        Builders<DepartmentSchema>.Filter.And(
                            Builders<DepartmentSchema>.Filter.Eq(f => f.DepartmentName, DepartmentName),
                            Builders<DepartmentSchema>.Filter.In("TeachersId", new [] { UserId })
                        ),
                        Builders<DepartmentSchema>.Filter.And(
                            Builders<DepartmentSchema>.Filter.Eq(f => f.DepartmentName, DepartmentName),
                            Builders<DepartmentSchema>.Filter.In("CreatorId", new [] { UserId })
                        )
                    )
                ).ToListAsync();

                if(result.Count == 0) { 
                    return new {
                        Status = StatusCodes.Status204NoContent,
                        Result = (Object)null!  
                    };
                }

                return new {
                    Status = StatusCodes.Status200OK,
                    Result = result
                };

            } catch {

                return new {
                    Status = StatusCodes.Status500InternalServerError,
                    Result = (Object?)null
                };
            }
        }
    }

    public async Task<int> DeletingPendingDepartment(DeletingApprovingPendingDepartment DTO) {

        try {

            await _Mongo.PendingDepartmentsCollection().FindOneAndDeleteAsync(
                Builders<PendingDepartmentSchema>.Filter.Eq(F => F.Id, DTO.PendingDepartmentId)
            );

            return StatusCodes.Status200OK;

        } catch {

            return StatusCodes.Status500InternalServerError;
        }
    }

    public async Task<int> ApprovingPendingDepartment(DeletingApprovingPendingDepartment DTO) {

        try {

            var PendingDepartmentList = await _Mongo.PendingDepartmentsCollection().Find(
                Builders<PendingDepartmentSchema>.Filter.Eq(F => F.Id, DTO.PendingDepartmentId)
            ).ToListAsync();

            var PendingDepartment = PendingDepartmentList.FirstOrDefault();
            await _Mongo.DepartmentCollection().InsertOneAsync(PendingDepartment!.Department!);
            await _Mongo.PendingDepartmentsCollection().FindOneAndDeleteAsync(
                Builders<PendingDepartmentSchema>.Filter.Eq(F => F.Id, DTO.PendingDepartmentId)
            );
            
            return StatusCodes.Status201Created;

        } catch {

            return StatusCodes.Status500InternalServerError;
        }
    }

    public async Task<object> GetDeansPendingDepartment(string UserId) {

        var User = await _UserManager.FindByIdAsync(UserId);

        try {

            var Result = await _Mongo.PendingDepartmentsCollection().Find(
                Builders<PendingDepartmentSchema>.Filter.Eq(F => F.RequestedBy, User!.Email)
            ).ToListAsync();

            return new {
                Status = StatusCodes.Status200OK,
                Data = Result
            };

        } catch {

            return new {
                Status = StatusCodes.Status200OK,
                Data = (Object)null!
            };

        }
    }

    public async Task<int> AddingMembersService(AddMembersInDepartmentDTO DTO) {

        try {

            if(DTO.Role == "student") {

                await _Mongo.DepartmentCollection().FindOneAndUpdateAsync(
                    Builders<DepartmentSchema>.Filter.Eq(x => x.DepartmentName, DTO.DepartmentName),
                    Builders<DepartmentSchema>.Update.PushEach(x => x.MembersId, DTO.UsersId)
                );

            } else {

                await _Mongo.DepartmentCollection().FindOneAndUpdateAsync(
                    Builders<DepartmentSchema>.Filter.Eq(x => x.DepartmentName, DTO.DepartmentName),
                    Builders<DepartmentSchema>.Update.PushEach(x => x.TeachersId, DTO.UsersId)
                );
            }

            return StatusCodes.Status200OK;

        }
        catch {

            return StatusCodes.Status500InternalServerError;
        }

    }
}