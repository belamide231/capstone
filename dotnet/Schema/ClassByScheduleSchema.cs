using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class ClassByScheduleSchema {

    [BsonElement("Schedule")]
    public string? Schedule { get; set; }

    [BsonElement("Posts")]
    public string? Posts { get; set; }

    [BsonElement("Tasks")]
    public List<TasksSchema>? Tasks { get; set; }

    [BsonElement("StudentsId")]
    public List<string>? StudentsId { get; set; }
}