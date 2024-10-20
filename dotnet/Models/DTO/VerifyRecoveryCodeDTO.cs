using System.Text.Json.Serialization;

public class VerifyRecoveryCodeDTO {
    

    [JsonPropertyName("email")]
    public string Email { get; set; }


    [JsonPropertyName("code")]
    public string Code { get; set; }
}