
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MongoDB.Bson;

public class UsersResult {    

    [JsonPropertyName("_id")]
    public ObjectId Id { get; set; }
    
    [JsonPropertyName("Email")]
    public string? Email { get; set; }

    [JsonPropertyName("Roles")]
    public List<string>? Roles { get; set; }
}