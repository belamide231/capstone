using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {

    private UsersServices _service;

    public UsersController(UsersServices service) {
        _service = service;
    }

    [Authorize(Policy = AdminHandler._Policy)]
    [HttpPost("allUsers")]
    public async Task<IActionResult> Users() {

        var result = await _service.GetAllUsers(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return Ok(result);
    }

    [HttpPost("saveChange")]
    public async Task<IActionResult> Save([FromBody] UpdateRoleDTO DTO) {

        dynamic result = await _service.ChangeRole(DTO);
        return StatusCode(result);
    }


    // WORKING
    [HttpPost("getUsersToAddInDepartment")]
    public async Task<IActionResult> GetUsersToAddInDepartment() {

        dynamic Result = await _service.GetUsersToAddInDepartmentService(Request.Query["department"]!, Request.Query["role"]!);
        return StatusCode(Result.Status, Result.Data);
    }
}

