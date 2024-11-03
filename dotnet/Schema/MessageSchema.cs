using System.Text.Json.Serialization;

public class MessageSchema {

    public DateTime Time { get; set; }

    [JsonPropertyName("conversationId")]
    public string? conversationId { get; set; }

    [JsonPropertyName("sender")]
    public string? userId { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("receivers")]
    public List<string>? receivers { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
    
}