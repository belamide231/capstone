using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

public class UserInterfaceController : ControllerBase
{
    public static string _WebDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/browser");

    [HttpGet("{*WebPath}")]
    public IActionResult Render(string WebPath) {

        if (string.IsNullOrEmpty(WebPath)) {
            WebPath = "index.html";
        }

        string FullPath = Path.Combine(_WebDirectory, WebPath);

        if (System.IO.File.Exists(FullPath)) {
            
            return PhysicalFile(FullPath, "text/html");
        }

        return PhysicalFile(Path.Combine(_WebDirectory, "index.html"), "text/html");
    }
}
