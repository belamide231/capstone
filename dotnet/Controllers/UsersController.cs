using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase {

    private UsersServices _service;

    public UsersController(UsersServices service) {
        _service = service;
    }

    // THIS API WILL REQUIRE ROLE IDENTITY AS AN ADMIN IN THE FUTURE
    [HttpPost("allUsers")]
    public async Task Users() {
        await _service.GetAllUsers();
    }
}

