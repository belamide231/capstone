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

    [Authorize]
    [Authorize(Policy = AdminHandler._Policy)]
    [HttpPost("request")]
    public async Task<IActionResult> GetRequest() {

        var claims = User.Claims;
        var claim = claims.FirstOrDefault();
        var userId = claim?.Value;

        dynamic result = await _Services!.GetDepartmentRequestService(userId!);
        return StatusCode(result.Status, result.Result);
    }

    [Authorize]
    [Authorize(Policy = UserHandler._Policy)]
    [HttpPost("getAllDepartments")]
    public async Task<IActionResult> GetDepartments() {

        dynamic result = await _Services!.GetDepartmentsService();
        return StatusCode(result.Status, result.Result);
    }

    [Authorize]
    [Authorize(Policy = UserHandler._Policy)]
    [HttpPost("getDepartment")]
    public async Task<IActionResult> GetDepartment() {

        dynamic result = await _Services!.GetDepartmentService(Request.Query["departmentName"]!, User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)!.Value);
        return StatusCode(result.Status, result.Result);
    }
}
