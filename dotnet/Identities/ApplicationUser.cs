using AspNetCore.Identity.Mongo.Model;

public class ApplicationUser : MongoUser {

    public List<DeviceIdSchema> DeviceIds { get; set; } = new List<DeviceIdSchema>();

    public ApplicationUser(string email) {
        
        Email = email;
        UserName = email;
        EmailConfirmed = true;
        Roles.Add("user");
    }
}