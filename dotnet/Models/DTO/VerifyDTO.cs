using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class VerifyDTO {
    
    [Required(ErrorMessage = "Email should not be blank.")]
    [MinLength(8, ErrorMessage = "Email contained 8 characters or more.")]
    [EmailAddress(ErrorMessage = "Email did not exist.")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}