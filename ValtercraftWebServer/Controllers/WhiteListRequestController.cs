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
            int userId = int.Parse(User.FindFirst("id")?.Value);
            WhiteListRequestDto? request = await _whiteListRequestService.CreateWhiteListRequest(userId, createWhiteListRequestDto);
            if (request == null)
                return BadRequest();
            return Ok(request);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWhiteListRequest(int id)
        {
            WhiteListRequestDto? request = await _whiteListRequestService.GetWhiteListRequest(id);
            if (request == null)
                return NotFound();
            return Ok(request);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("all/{id?}")]
        public async Task<IActionResult> GetAlWhiteListRequests(int? id)
        {
            List<WhiteListRequestDto?> requests = await _whiteListRequestService.GetAllWhiteListRequests(id);
            return Ok(requests);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveWhiteListRequest(int id)
        {
            bool result = await _whiteListRequestService.SetRequestStatus(id, 1);
            if (result == false)
                return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("decline/{id}")]
        public async Task<IActionResult> DeclineWhiteListRequest(int id)
        {
            bool result = await _whiteListRequestService.SetRequestStatus(id, 2);
            if (result == false)
                return BadRequest();
            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWhiteListRequest(int id)
        {
            bool result = await _whiteListRequestService.DeleteWhiteListRequest(id);
            if (result == false)
                return BadRequest();
            return Ok();
        }
    }
}
