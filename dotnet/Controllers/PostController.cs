using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase {

    private readonly PostServices _Services;

    public PostController(PostServices __Services) {
        _Services = __Services;
    }

    [HttpPost("postInHome")]
    public async Task<IActionResult> PostInHome([FromBody] PostInHomeDTO DTO) {
        dynamic Result = await _Services.PostingInHome(DTO, User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return StatusCode(Result.Status, Result.Data);
    }

    [Authorize(Policy = UserHandler._Policy)]
    [HttpPost("getHomePosts")] 
    public async Task<IActionResult> GetHomePosts() {
        dynamic Result = await _Services.GetHomePosts();
        return StatusCode(Result.Status, Result.Data);
    }

    [Authorize(Policy = AdminOrDeanOrTeacherHandler._Policy)]
    [HttpPost("getHomePendingPosts")]
    public async Task<IActionResult> GetHomePendingPost() {
        dynamic Result = await _Services.GetHomePendingPostService();
        return StatusCode(Result.Status, Result.Data);
    }

    [HttpPost("getDepartmentPosts")]
    public async Task<IActionResult> GetDepartmentPosts() {
        return Ok();
    }

    [HttpPost("getClassPosts")]
    public async Task<IActionResult> GetClassPosts() {
        return Ok();
    }

    [HttpPost("postInPortal")]
    public async Task<IActionResult> PostInPortal() {
        return Ok();
    }

    [HttpPost("postInDepartment")]
    public async Task<IActionResult> PostInDepartment() {
        return Ok();
    }

    [HttpPost("postInClass")]
    public async Task<IActionResult> PostInClass() {
        return Ok();
    }
}