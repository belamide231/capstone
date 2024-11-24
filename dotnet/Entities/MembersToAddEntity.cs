using MongoDB.Bson.Serialization.Attributes;

public class MembersToAddEntity {

    [BsonElement("MembersId")]
    public List<string> ListOfStudentsId { get; set; }

    [BsonElement("TeachersId")]
    public List<string> ListOfTeachersId { get; set; }
}