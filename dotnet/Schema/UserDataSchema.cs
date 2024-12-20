using MongoDB.Bson.Serialization.Attributes;

public class UsersDataSchema {

    [BsonId]
    public string? UserId { get; set; }

    [BsonElement("StacksOfConversations")]
    public List<string> StacksOfConversations { get; set; }

    public UsersDataSchema(string userId) {
        UserId = userId;
        StacksOfConversations = new List<string>();
    }
}