using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using MongoDB.Driver;

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
}