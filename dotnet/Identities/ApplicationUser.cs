using AspNetCore.Identity.Mongo.Model;

public class ApplicationUser : MongoUser {


    public ApplicationUser(string email) {
        Email = email;
        UserName = email;
        EmailConfirmed = true;
    }
}