using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson.IO;
using MongoDB.Driver;

public class UsersServices {

    private Mongo? _mongo;
    private UserManager<ApplicationUser> _userManager;

    public UsersServices(Mongo mongo, UserManager<ApplicationUser> userManager) {
        _mongo = mongo;
        _userManager = userManager;
    }

    public async Task GetAllUsers() {
        var users = await _mongo!.ApplicationUsers().Find(
            Builders<ApplicationUser>.Filter.Empty
        ).Project<UsersResult>(
            Builders<ApplicationUser>.Projection
                .Include(u => u.Id)
                .Include(u => u.Email)
                .Include(u => u.Roles)
        ).Limit(30).ToListAsync();

        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented));
    }

}