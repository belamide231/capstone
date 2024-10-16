using System.Text.Json.Serialization;

public class VerifyLoginCodeDTO {
    
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("trust")]
    public bool Trust { get; set; } = false;

    [JsonPropertyName("deviceIdIdentifier")]
    public string DeviceIdIdentifier { get; set; } = string.Empty;

    [JsonPropertyName("deviceId")]
    public string DeviceId { get; set; } = string.Empty;
}