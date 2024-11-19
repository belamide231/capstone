
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UsersEntity {    
    
    [BsonElement("Email")]
    public string? Email { get; set; }

    [BsonElement("Roles")]
    public List<string>? Roles { get; set; }
}