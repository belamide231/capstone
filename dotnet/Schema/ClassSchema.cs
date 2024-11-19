using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class ClassSchema {

    [BsonElement("ClassName")]
    public string? ClassName { get; set; }

    [BsonElement("CreatorId")]
    public string? CreatorId { get; set; }

    [BsonElement("ClassBySchedule")]
    public ClassByScheduleSchema? ClassBySchedule { get; set; }
}