using System.Text.Json.Serialization;

public class MessageStatusModel {

    [JsonPropertyName("conversationId")]
    public string? conversationId { get; set; }

    [JsonPropertyName("userId")]
    public string? userId { get; set; }
    
    public DateTime seenTime = DateTime.UtcNow;
}