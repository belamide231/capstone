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

    public async Task<List<UsersResult>> GetAllUsers(string role) {

        var result = new List<UsersResult>();

        if(role == "admin") {

            result = await _mongo!.ApplicationUsers().Find(
                Builders<ApplicationUser>.Filter.Ne(u => u.Roles, ["admin"])
            ).Project<UsersResult>(
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
            ).Project<UsersResult>(
                Builders<ApplicationUser>.Projection
                    .Exclude(f => f.Id)
                    .Include(f => f.Email)
                    .Include(f => f.Roles)
            ).Limit(30)
            .ToListAsync();
        }

        return result;
    }

    public async Task ChangeRole(string email, string role) {
        var user = await _userManager.FindByEmailAsync(email);
        user!.Roles = [role];
        await _userManager.UpdateAsync(user); 
    } 
}