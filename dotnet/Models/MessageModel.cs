using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class ReceiverModel {

    [JsonPropertyName("userId")]
    public string? userId { get; set; }

    [JsonPropertyName("status")]
    public string? status { get; set; }
} 

public class MessageModel { 

    [JsonPropertyName("conversationId")]
    public string? conversationId { get; set; }
    
    public DateTime? time = DateTime.UtcNow;

    [JsonPropertyName("sender")]
    public string? sender { get; set; }

    [JsonPropertyName("receiver")]
    public List<ReceiverModel>? receiver { get; set; }

    [JsonPropertyName("message")]
    public string? message { get; set; }
}