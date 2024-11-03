using System.Text.Json.Serialization;

public class SeenByIndexSchema {
    
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }

    // [JsonPropertyName("time")]
    // public DateTime Time { get; set; }
}

public class ConversationSchema {

    [JsonPropertyName("members")]
    public List<string>? Members { get; set; }

    [JsonPropertyName("seenByIndex")]
    public List<SeenByIndexSchema>? SeenByIndex { get; set; }

    [JsonPropertyName("messages")]
    public List<MessageSchema>? Messages { get; set; }
}