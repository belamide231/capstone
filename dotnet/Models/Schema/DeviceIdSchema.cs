using MongoDB.Bson.Serialization.Attributes;

public class DeviceIdSchema {
    
    [BsonElement("DeviceIdIdentifier")]
    public string DeviceIdIdentifier { get; set; } = string.Empty;
    
    [BsonElement("DeviceId")]
    public string DeviceId { get; set; } = string.Empty;

    public DeviceIdSchema(string deviceIdIdentifier, string deviceId) {
        DeviceIdIdentifier = deviceIdIdentifier;
        DeviceId = deviceId;
    }
}