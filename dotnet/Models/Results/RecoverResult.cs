using System.Text.Json.Serialization;


public class RecoverResult: StatusObject {

    [JsonPropertyName("deviceIdIdentifier")]
    public string deviceIdIdentifier { get; set; } 

    [JsonPropertyName("deviceId")]
    public string deviceId { get; set; }

    public RecoverResult() : base(StatusCodes.Status202Accepted) {

        deviceIdIdentifier = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        deviceId = Guid.NewGuid().ToString() + "-" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + "-" + Guid.NewGuid();
    }
}