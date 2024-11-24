using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UsersToAddInDepartmentEntity {

    [BsonElement("_id")]
    public ObjectId Id { get; set; }

    [BsonElement("Email")]
    public string Email { get; set; }
}