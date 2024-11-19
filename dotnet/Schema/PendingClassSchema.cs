using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class PendingClassSchema {

    [BsonElement("Class")]
    public ClassSchema? Class { get; set; }

    [BsonElement("RequestedBy")]
    public string? CreatorId { get; set; }
}