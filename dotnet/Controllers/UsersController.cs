using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Org.BouncyCastle.Ocsp;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {

    private UsersServices _service;

    public UsersController(UsersServices service) {
        _service = service;
    }

    // THIS API WILL REQUIRE ROLE IDENTITY AS AN ADMIN IN THE FUTURE
    [HttpPost("allUsers")]
    public async Task<IActionResult> Users() {
        var result = await _service.GetAllUsers(Request.Query["role"]!);
        return Ok(result);
    }

    [HttpPost("saveChange")]
    public async Task<IActionResult> Save() {
        Console.WriteLine(Request.Query["email"]);
        Console.WriteLine(Request.Query["role"]);
        await _service.ChangeRole(Request.Query["email"]!, Request.Query["role"]!);
        return Ok();
    }
}

