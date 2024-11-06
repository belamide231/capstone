using MongoDB.Bson;
using MongoDB.Driver;


public class Mongo {


    private readonly IMongoDatabase _mongo;
    public static string _applicationUsers = "Users";
    public static string _applicationRoles = "Roles";
    public static string _usersData = "UsersData";
    public static string _applicationConversations = "Conversations";
    public Mongo() {

        var connectionString = MongoUrl.Create(EnvHelper._MongoUrl);
        _mongo = new MongoClient(connectionString).GetDatabase(connectionString.DatabaseName);
    }
    

    public IMongoCollection<ApplicationUser> ApplicationUsers() => _mongo.GetCollection<ApplicationUser>(_applicationUsers);
    public IMongoCollection<ApplicationRole> ApplicationRoles() => _mongo.GetCollection<ApplicationRole>(_applicationRoles);
    public IMongoCollection<UsersDataSchema> UsersDataCollection() => _mongo.GetCollection<UsersDataSchema>(_usersData);
    public IMongoCollection<ConversationSchema> ConversationCollection() => _mongo.GetCollection<ConversationSchema>(_applicationConversations);
}