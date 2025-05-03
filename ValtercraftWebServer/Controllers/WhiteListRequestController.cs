using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ValtercraftWebServer.DTO;
using ValtercraftWebServer.Models;
using ValtercraftWebServer.Services;

namespace ValtercraftWebServer.Controllers
{
    [Route("wlreq")]
    [ApiController]
    public class WhiteListRequestController : ControllerBase
    {
        private readonly WhiteListRequestService _whiteListRequestService;
        private readonly UsersService _usersService;

        public WhiteListRequestController(WhiteListRequestService whiteListRequestService, UsersService usersService)
        {
            _whiteListRequestService = whiteListRequestService;
            _usersService = usersService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateWhiteListRequest([FromBody] CreateWhiteListRequestDto createWhiteListRequestDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            WhiteListRequestDto? request = await _whiteListRequestService.CreateWhiteListRequest(userId, createWhiteListRequestDto);
            if (request == null)
                return BadRequest();
            return Ok(request);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhiteListRequest(int id)
        {
            var request = await _whiteListRequestService.GetWhiteListRequest(id);
            if (request == null)
                return NotFound();
            return Ok(request);
        }
    }
}
