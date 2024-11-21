using Newtonsoft.Json;

public class CancelStudentPendingPostDTO {

    [JsonProperty("studentsPendingPostId")]
    public string? StudentsPendingPostId { get; set; } 
}