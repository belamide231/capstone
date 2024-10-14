using System.Text.Json.Serialization;


public class CreateAccountDTO {

    [JsonPropertyName("email")]
    public string? Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string? Password { get; set; } = string.Empty;

    [JsonPropertyName("trust")]
    public bool? Trust { get; set; } = false;
}