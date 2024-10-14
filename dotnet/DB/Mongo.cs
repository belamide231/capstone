using MongoDB.Driver;


public class Mongo {


    private readonly IMongoDatabase _mongo;
    public static string _applicationUsers = "Users";
    public static string _applicationRoles = "Roles";
    public Mongo() {

        var connectionString = MongoUrl.Create(EnvHelper._MongoUrl);
        _mongo = new MongoClient(connectionString).GetDatabase(connectionString.DatabaseName);
    }
    

    public IMongoCollection<ApplicationUser> ApplicationUsers() => _mongo.GetCollection<ApplicationUser>(_applicationUsers);
    public IMongoCollection<ApplicationRole> ApplicationRoles() => _mongo.GetCollection<ApplicationRole>(_applicationRoles);
}