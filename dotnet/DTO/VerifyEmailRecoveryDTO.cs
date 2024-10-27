using System.Text.Json.Serialization;

public class VerifyEmailRecoveryDTO {

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}