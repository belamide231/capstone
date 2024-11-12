using System.ComponentModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class CreatingDepartmentDTO {

    [JsonPropertyName("departmentName")]
    public string? DepartmentName { get; set; }
}