using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class PendingDepartmentSchema {

    [BsonId]
    public string Id { get; set; }

    [BsonElement("Department")] 
    public DepartmentSchema? Department { get; set; }

    [BsonElement("RequestedBy")]
    public string? RequestedBy { get; set; }

    public PendingDepartmentSchema(DepartmentSchema _Department, string _RequestedBy) {
        Id = ObjectId.GenerateNewId().ToString();
        RequestedBy = _RequestedBy;
        Department = _Department;
    }
}