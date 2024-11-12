using MongoDB.Bson.Serialization.Attributes;

public class PendingDepartmentSchema {

    [BsonElement("Department")]
    public DepartmentSchema? Department { get; set; }

    [BsonElement("RequestedBy")]
    public string? RequestedBy { get; set; }

    public PendingDepartmentSchema(DepartmentSchema _Department, string _RequestedBy) {
        RequestedBy = _RequestedBy;
        Department = _Department;
    }
}