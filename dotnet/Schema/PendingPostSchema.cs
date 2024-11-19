using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class PendingPostSchema {

    [BsonElement("Post")]
    public PostSchema? Post { get; set; }

    [BsonElement("RequestedBy")]
    public string? RequestedBy { get; set; }

    [BsonElement("Type")]
    public string? Type { get; set; }

    [BsonElement("In")]
    public string? In { get; set; }

    public PendingPostSchema(PostSchema _Post, string _RequestedBy, string _Type, string _In) {
        Post = _Post;
        RequestedBy = _RequestedBy;
        Type = _Type;
        In = _In;
    }
}