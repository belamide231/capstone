using System.Text.Json.Serialization;

public class IncrementFailAttemptDTO {

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}