using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

public class AudienceLatestSeenMessageModel {

    [BsonElement("audienceId")]
    public string? AudienceId { get; set; }

    [BsonElement("messageId")]
    public string? MessageId { get; set; }
}

public class ConversationSchema {

    [BsonId]
    public string ConversationId { get; set; }

    [BsonElement("audience")]
    public List<string>? Audience { get; set; }

    [BsonElement("messageSeenByAudience")]
    public List<AudienceLatestSeenMessageModel> AudienceLatestSeenMessage { get; set; }

    [BsonElement("messages")]
    public List<MessageSchema>? Messages { get; set; }

    public ConversationSchema(List<string> audience) {
        ConversationId = ObjectId.GenerateNewId().ToString();
        Audience = audience;
        AudienceLatestSeenMessage = new List<AudienceLatestSeenMessageModel>();
        Messages = new List<MessageSchema>();
    }
}