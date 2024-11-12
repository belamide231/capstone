using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase {

    private DepartmentServices? _Services;

    public DepartmentController(DepartmentServices __Services) {
        _Services = __Services;
    }

    [Authorize]
    [Authorize(Policy = AdminOrDeanHandler._Policy)]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatingDepartmentDTO DTO) {

        var Claims = User.Claims;
        var Claim = Claims.FirstOrDefault();
        var UserId = Claim!.Value;
        
        await _Services!.CreateDepartment(DTO, UserId);
        return Ok();
    }
}
