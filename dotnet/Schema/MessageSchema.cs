using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MessageSchema {

    [BsonElement("created")]
    public DateTime Created { get; set; }

    [BsonElement("conversationId")]
    public string? ConversationId { get; set; }

    [BsonId]
    public string? MessageId { get; set; }

    [BsonElement("sender")]
    public string? Sender { get; set; }

    [BsonElement("seen")]
    public List<string>? Seen { get; set; }

    [BsonElement("status")]
    public string? Status { get; set; }

    [BsonElement("message")]
    public string? Message { get; set; }
    
    public MessageSchema(string conversationId, string sender, string message) {
        TimeZoneInfo philippineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");
        DateTime philippineTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, philippineTimeZone);

        Created = philippineTime;
        ConversationId = conversationId;
        MessageId = ObjectId.GenerateNewId().ToString();
        Sender = sender;
        Seen = new List<string>();
        Status = "Sent";
        Message = message;
    }
}