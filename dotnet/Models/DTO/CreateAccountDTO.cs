using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;


public class CreateAccountDTO {

    [JsonPropertyName("email")]
    public string? Email { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string? Password { get; set; } = string.Empty;

    [JsonPropertyName("trust")]
    public bool Trust { get; set; } = false;

    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; } = string.Empty;

    [JsonPropertyName("deviceIdIdentifier")]
    public string DeviceIdIdentifier { get; set; } = string.Empty;
}