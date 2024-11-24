using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using StackExchange.Redis;

public class UsersServices {

    private Mongo? _mongo;
    private UserManager<ApplicationUser> _userManager;

    public UsersServices(Mongo mongo, UserManager<ApplicationUser> userManager) {
        _mongo = mongo;
        _userManager = userManager;
    }

    public async Task<List<UsersEntity>> GetAllUsers(string UserId) {

        var role = (await _userManager.FindByIdAsync(UserId))!.Roles.FirstOrDefault();
        var result = new List<UsersEntity>();

        if(role == "admin") {

            result = await _mongo!.ApplicationUsers().Find(
                Builders<ApplicationUser>.Filter.Ne(u => u.Roles, ["admin"])
            ).Project<UsersEntity>(
                Builders<ApplicationUser>.Projection
                    .Exclude(f => f.Id)
                    .Include(f => f.Email)
                    .Include(f => f.Roles)
            ).Limit(30).ToListAsync();
        
        } else if(role == "dean") {

            result = await _mongo!.ApplicationUsers().Find(
                Builders<ApplicationUser>.Filter.And(
                    Builders<ApplicationUser>.Filter.Ne(f => f.Roles, ["admin"]),
                    Builders<ApplicationUser>.Filter.Ne(f => f.Roles, ["dean"])
                )
            ).Project<UsersEntity>(
                Builders<ApplicationUser>.Projection
                    .Exclude(f => f.Id)
                    .Include(f => f.Email)
                    .Include(f => f.Roles)
            ).Limit(30)
            .ToListAsync();
        }

        return result;
    }

    public async Task<int> ChangeRole(UpdateRoleDTO DTO) {

        var user = await _userManager.FindByEmailAsync(DTO.Email!);

        try {

            user!.Roles = [DTO.Role];
            await _userManager.UpdateAsync(user); 

            return StatusCodes.Status200OK;

        } catch {

            return StatusCodes.Status500InternalServerError;
        }
    }


    // WORKING
    public async Task<object> GetUsersToAddInDepartmentService(string DepartmentName, string Role) {

        try {

            var ListOfDepartmentMembers = await _mongo!.DepartmentCollection().Find(
                Builders<DepartmentSchema>.Filter.Eq(x => x.DepartmentName, DepartmentName)
            ).Project<MembersToAddEntity>(
                Builders<DepartmentSchema>.Projection
                    .Include(x => x.MembersId)
                    .Include(x => x.TeachersId)
                    .Exclude("_id")
            ).ToListAsync();

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(ListOfDepartmentMembers, Newtonsoft.Json.Formatting.Indented));

            var Exception = new List<string>();
            if(Role == "student") {

                Exception = ListOfDepartmentMembers.FirstOrDefault()?.ListOfStudentsId;
            } else {

                Exception = ListOfDepartmentMembers.FirstOrDefault()?.ListOfTeachersId;
            }

            Console.WriteLine(Role);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Exception, Newtonsoft.Json.Formatting.Indented));

            dynamic Result = new List<object>();

            if (Exception!.Count == 0) {

                Result = (await _mongo!.ApplicationUsers().Find(
                    Builders<ApplicationUser>.Filter.Eq(x => x.Roles, [Role])
                ).Project<UsersToAddInDepartmentEntity>(
                    Builders<ApplicationUser>.Projection
                        .Include(x => x.Id)
                        .Include(x => x.Email)
                ).ToListAsync()).Select(x => new { Id = x.Id.ToString(), x.Email }).ToList();
            }
            else {

                Result = (await _mongo!.ApplicationUsers().Find(
                    Builders<ApplicationUser>.Filter.And(
                        Builders<ApplicationUser>.Filter.Eq(x => x.Roles, [Role]),
                        Builders<ApplicationUser>.Filter.Nin(x => x.Id.ToString(), Exception) 
                    )
                ).Project<UsersToAddInDepartmentEntity>(
                    Builders<ApplicationUser>.Projection
                        .Include(x => x.Id)
                        .Include(x => x.Email)
                ).ToListAsync()).Select(x => new { Id = x.Id.ToString(), x.Email }).ToList();
            }

            return new {
                Status = StatusCodes.Status200OK,
                Data = Result
            };

        } catch {

            return new {
                Status = 500,
                Data = (object)null!
            };
        }
    }
}