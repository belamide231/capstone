using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class PostInHomeDTO {

    [JsonPropertyName("Description")]
    public string? Description { get; set; }
 
    [JsonPropertyName("In")]
    public string? In { get; set; }
}