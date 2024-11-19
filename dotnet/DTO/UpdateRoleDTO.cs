using System.Text.Json.Serialization;

public class UpdateRoleDTO {

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }
}