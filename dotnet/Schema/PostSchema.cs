using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class PostSchema {

    [BsonElement("PostedBy")]
    public string? PostedBy { get; set; }

    [BsonElement("Type")]
    public string? Type { get; set; }

    [BsonElement("Images")]
    public List<string>? Images { get; set; }

    [BsonElement("Description")]
    public string? Description { get; set; }

    [BsonElement("ThumbsUp")]
    public List<string>? ThumbsUp { get; set; } // USERID

    [BsonElement("ThumbsDown")]
    public List<string>? ThumbsDown { get; set; } // USERID

    [BsonElement("Comments")]
    public List<CommentSchema>? Comments { get; set; } 

    [BsonElement("In")]
    public string In { get; set; }

    public PostSchema(string _Description, string _Type, string _PostedBy, string _In) {

        Type = _Type;
        Images = new List<string>();
        Description = _Description;
        ThumbsUp = new List<string>();
        ThumbsDown = new List<string>(); 
        Comments = new List<CommentSchema>();
        PostedBy = _PostedBy;
        In = _In;
    }
}