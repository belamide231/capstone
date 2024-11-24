using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class AddMembersInDepartmentDTO {

    [JsonPropertyName("departmentName")]
    public string DepartmentName { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("usersId")]
    public List<string> UsersId { get; set; }
}