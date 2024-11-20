using System.Text.Json.Serialization;

public class DeletingApprovingPendingDepartment {

    [JsonPropertyName("pendingDepartmentId")]
    public string? PendingDepartmentId { get; set; }
}