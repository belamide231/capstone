using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;
using MimeKit;

public class MessageModel {

    [JsonPropertyName("sender")]
    public string? Sender { get; set; }

    [JsonPropertyName("receivers")]
    public List<string>? Receivers { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}