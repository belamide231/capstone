using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class NewPasswordRecoveryDTO {

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("trust")]
    public bool Trust { get; set; } = false;

    [JsonPropertyName("deviceIdIdentifier")]
    public string DeviceIdIdentifier { get; set; } = string.Empty;

    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; } = string.Empty;


}