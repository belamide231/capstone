using System.Text.Json.Serialization;

public class VerifyCredentialDTO {

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("trust")]
    public bool Trust { get; set; } = false;

    [JsonPropertyName("deviceIdIdentifier")]
    public string DeviceIdIdentifier { get; set; } = string.Empty;

    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; } = string.Empty;
}