using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class TasksSchema {

    [BsonElement("TimeStamp")]
    public DateTime TimeStamp { get; set; }

    [BsonElement("TasksName")]
    public string? TasksName { get; set; }

    [BsonElement("Score")]
    public string? Score { get; set; }

    [BsonElement("TurnedIn")]
    public List<string>? TurnedIn { get; set; } // USERID

    [BsonElement("ClosedAt")]
    public DateTime ClosedAt { get; set; }
}