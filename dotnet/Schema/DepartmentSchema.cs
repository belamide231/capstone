using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class DepartmentSchema {

    [BsonElement("DepartmentName")]
    public string? DepartmentName { get; set; }

    [BsonElement("DepartmentDescription")]
    public string DepartmentDescription { get; set; }

    [BsonElement("MembersId")]
    public List<string>? MembersId { get; set; }

    [BsonElement("CreatorId")]
    public string? CreatorId { get; set; }

    [BsonElement("TeachersId")]
    public List<string>? TeachersId { get; set; }

    [BsonElement("ClassroomsId")]
    public List<string>? ClassroomsId { get; set; }

    [BsonElement("DepartmentPosts")]
    public List<PostSchema>? DepartmentPosts { get; set; }

    public DepartmentSchema(string _DepartmentName, string _DepartmentDescription, string _CreatorId) {
        DepartmentName = _DepartmentName;
        DepartmentDescription = _DepartmentDescription;
        MembersId = new List<string>();
        CreatorId = _CreatorId;
        TeachersId = new List<string>();
        ClassroomsId = new List<string>();
        DepartmentPosts = new List<PostSchema>();
    }
}