using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValtercraftWebServer.Services;

namespace ValtercraftWebServer.Controllers
{
    [Route("rcon")]
    [ApiController]
    public class RconController : ControllerBase
    {
        private readonly RconService _rconService;

        public RconController(RconService rconService)
        {
            _rconService = rconService;
        }

        [HttpGet("online")]
        public async Task<IActionResult> GetOnline()
        {
            int? response = await _rconService.GetOnline();
            if (response == null)
                BadRequest();
            return Ok(new { online = response });
        }
    }
}
