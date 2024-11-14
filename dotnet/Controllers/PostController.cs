using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase {

    private readonly PostServices _Services;

    public PostController(PostServices __Services) {
        _Services = __Services;
    }

    [HttpPost("getPortalPosts")] 
    public async Task<IActionResult> GetPortalPosts() {
        return Ok();
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