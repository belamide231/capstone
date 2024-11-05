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

        Initializations();
    }
    

    public IMongoCollection<ApplicationUser> ApplicationUsers() => _mongo.GetCollection<ApplicationUser>(_applicationUsers);
    public IMongoCollection<ApplicationRole> ApplicationRoles() => _mongo.GetCollection<ApplicationRole>(_applicationRoles);
    public IMongoCollection<UsersDataSchema> UserDataCollection() => _mongo.GetCollection<UsersDataSchema>(_usersData);
    public IMongoCollection<ConversationSchema> ConversationCollection() => _mongo.GetCollection<ConversationSchema>(_applicationConversations);
    public void Initializations() {
        var indexOptions = new CreateIndexOptions { Background = true };
        var indexDefinition = Builders<ConversationSchema>.IndexKeys.Descending("messages.created");
        ConversationCollection().Indexes.CreateOne(new CreateIndexModel<ConversationSchema>(indexDefinition, indexOptions));


        // var filter = Builders<ConversationSchema>.Filter.Eq(f => f.ConversationId, "672a0ae7b6339ba4caba2a0a");
        // var projection = Builders<ConversationSchema>.Projection
        //     .Include(c => c.ConversationId)
        //     .Include(c => c.Audience)
        //     .Include(c => c.AudienceLatestSeenMessage)
        //     .Slice(c => c.Messages, 3);
        // var result = ConversationCollection().Find(filter).Project<ConversationSchema>(projection).FirstOrDefault();
        // Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result.Messages, Newtonsoft.Json.Formatting.Indented));
    }

}