using MongoDB.Bson.Serialization.Attributes;

public class CommentSchema {

    [BsonElement("Type")]
    public string? Type { get; set; }

    [BsonElement("Comment")]
    public string? Comment { get; set; }

    [BsonElement("ThumbsUp")]
    public List<string>? ThumbsUp { get; set; }

    [BsonElement("ThumbsDown")]
    public List<string>? ThumbsDown { get; set; }
}