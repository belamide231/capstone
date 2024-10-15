using System.Text.Json.Serialization;

public class UpdateCodeDTO {

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("match")]
    public bool Match { get; set; } = false;
}