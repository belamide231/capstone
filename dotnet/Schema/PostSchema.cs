using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PostSchema {

    [BsonElement("Images")]
    public List<string>? Images { get; set; }

    [BsonElement("Description")]
    public string? Description { get; set; }

    [BsonElement("ThumbsUp")]
    public string? ThumbsUp { get; set; }

    [BsonElement("ThumbsDown")]
    public string? ThumbsDown { get; set; }

    [BsonElement("Comments")]
    public List<BsonDocument>? Comments { get; set; } 
}