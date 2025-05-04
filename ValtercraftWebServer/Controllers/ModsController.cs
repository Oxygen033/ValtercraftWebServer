using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ValtercraftWebServer.Controllers
{
    [Route("mods")]
    [ApiController]
    public class ModsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public ModsController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult DownloadMods()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "Utils", "mods.zip"); //todo: change to secrets

            if (!System.IO.File.Exists(filePath))
                return NotFound(new { error = "Mods directory not found" });

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = "mods.zip";

            return File(fileBytes, "application/zip", fileName);
        }
    }
}
