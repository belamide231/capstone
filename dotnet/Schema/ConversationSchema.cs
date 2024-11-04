using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ConversationSchema {

    [BsonId]
    public ObjectId? ConversationId { get; set; }

    [JsonPropertyName("audience")]
    public List<string>? Audience { get; set; }

    [JsonPropertyName("messages")]
    public List<MessageSchema>? Messages { get; set; }

    public ConversationSchema(List<string> audience) {
        ConversationId = ObjectId.GenerateNewId();
        Audience = audience;
        Messages = new List<MessageSchema>();
    }
}