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

    //[Authorize(Policy = StudentHandler._Policy)]
    [HttpPost("getStudentPendingPostInHome")]
    public async Task<IActionResult> GetStudentsPendingPostInHome() {
        dynamic Result = await _Services.GetStudentsPendingPostInHome(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return StatusCode(Result.Status, Result.Data);
    }

    [HttpPost("cancelStudentPendingPost")]
    public async Task<IActionResult> CancelStudentPendingPost([FromBody] CancelStudentPendingPostDTO DTO) {

        dynamic Result = await _Services.CancelStudentPendingPostService(DTO);
        return StatusCode(Result);
    }

    [HttpPost("getAllRequestPostInHome")]
    public async Task<IActionResult> GetAllRequestPostInHome() {

        dynamic Result = await _Services.GetAllRequestPostInHomeService();
        return StatusCode(Result.Status, Result.Data);
    }

    [HttpPost("approvePendingPostInHome")]
    public async Task<IActionResult> ApprovePendingPostInHome([FromBody] CancelStudentPendingPostDTO DTO) {

        dynamic Result = await _Services.ApprovePendingPostInHome(DTO);
        return StatusCode(Result);
    }
}