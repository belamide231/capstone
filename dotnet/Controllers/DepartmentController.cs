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
        
        await _Services!.CreateDepartmentService(DTO, User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok();
    }

    [Authorize]
    [Authorize(Policy = AdminHandler._Policy)]
    [HttpPost("getPendingDepartments")]
    public async Task<IActionResult> GetRequest() {

        dynamic result = await _Services!.GetDepartmentRequestService(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
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

    [Authorize]
    [Authorize(Policy = AdminOrDeanHandler._Policy)]
    [HttpPost("deletePendingDepartment")]
    public async Task<IActionResult> DeletePendingDepartment([FromBody] DeletingApprovingPendingDepartment DTO) {

        dynamic result = await _Services!.DeletingPendingDepartment(DTO);
        return StatusCode(result);
    }

    [Authorize]
    [Authorize(Policy = AdminHandler._Policy)]
    [HttpPost("approvePendingDepartment")]
    public async Task<IActionResult> ApprovePendingeDepartment([FromBody] DeletingApprovingPendingDepartment DTO) {

        dynamic result = await _Services!.ApprovingPendingDepartment(DTO);
        return StatusCode(result);
    }

    [Authorize]
    [Authorize(Policy = DeanHandler._Policy)]
    [HttpPost("getDeansPendingDepartment")]
    public async Task<IActionResult> GetDeansPendingDepartment() {

        dynamic result = await _Services!.GetDeansPendingDepartment(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return StatusCode(result.Status, result.Data);
    }

    [HttpPost("addMembersInDepartment")]
    public async Task<IActionResult> AddMembersInDepartmentControl([FromBody] AddMembersInDepartmentDTO DTO) {
        int result = await _Services!.AddingMembersService(DTO);
        return StatusCode(result);
    }
}
