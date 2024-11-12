using MongoDB.Bson;
using MongoDB.Driver;


public class Mongo {


    private readonly IMongoDatabase _Mongo;
    public static string _ApplicationUsers = "Users";
    public static string _ApplicationRoles = "Roles";
    public static string _UsersData = "UsersData";
    public static string _ApplicationConversations = "Conversations";
    public static string _ApplicationDepartments = "Departments";


    public static string _ApplicationPendingDepartments = "PendingDepartments";
    public Mongo() {

        var ConnectionString = MongoUrl.Create(EnvHelper._MongoUrl);
        _Mongo = new MongoClient(ConnectionString).GetDatabase(ConnectionString.DatabaseName);
    }
    

    public IMongoCollection<ApplicationUser> ApplicationUsers() => _Mongo.GetCollection<ApplicationUser>(_ApplicationUsers);
    public IMongoCollection<ApplicationRole> ApplicationRoles() => _Mongo.GetCollection<ApplicationRole>(_ApplicationRoles);
    public IMongoCollection<UsersDataSchema> UsersDataCollection() => _Mongo.GetCollection<UsersDataSchema>(_UsersData);
    public IMongoCollection<ConversationSchema> ConversationCollection() => _Mongo.GetCollection<ConversationSchema>(_ApplicationConversations);
    public IMongoCollection<DepartmentSchema> DepartmentCollection() => _Mongo.GetCollection<DepartmentSchema>(_ApplicationDepartments);
    public IMongoCollection<PendingDepartmentSchema> PendingDepartmentsCollection() => _Mongo.GetCollection<PendingDepartmentSchema>(_ApplicationPendingDepartments);
}