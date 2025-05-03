using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValtercraftWebServer.Services;
using ValtercraftWebServer.Data;
using ValtercraftWebServer.Models;
using System.Composition.Convention;
using ValtercraftWebServer.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ValtercraftWebServer.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            User? user = await _usersService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId != id)
                return Forbid();

            User? user = await _usersService.UpdateUser(id, updateUserDto);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId != id)
                return Forbid();

            bool result = await _usersService.DeleteUser(id);

            if (result == false)
                return StatusCode(500);

            return Ok();
        }
    }
}
