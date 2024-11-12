using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class DepartmentSchema {

    [BsonElement("DepartmentName")]
    public string? DepartmentName { get; set; }

    [BsonElement("MembersId")]
    public List<string>? MembersId { get; set; }

    [BsonElement("CreatorId")]
    public string? CreatorId { get; set; }

    [BsonElement("DepartmentPosts")]
    public List<PostSchema>? DepartmentPosts { get; set; }

    public DepartmentSchema(string _DepartmentName, string _CreatorId) {
        DepartmentName = _DepartmentName;
        MembersId = new List<string>();
        CreatorId = _CreatorId;
        DepartmentPosts = new List<PostSchema>();
    }
}