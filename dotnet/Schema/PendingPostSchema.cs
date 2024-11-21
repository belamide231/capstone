using MongoDB.Bson.Serialization.Attributes;

public class PendingPostSchema {

    [BsonElement("_id")]
    public string Id { get; set; }

    [BsonElement("Post")]
    public PostSchema? Post { get; set; }

    [BsonElement("RequestedBy")]
    public string? RequestedBy { get; set; }

    [BsonElement("Type")]
    public string? Type { get; set; }

    [BsonElement("In")]
    public string? In { get; set; }

    public PendingPostSchema(PostSchema _Post, string _RequestedBy, string _Type, string _In) {
        Id = Guid.NewGuid().ToString();
        Post = _Post;
        RequestedBy = _RequestedBy;
        Type = _Type;
        In = _In;
    }
}