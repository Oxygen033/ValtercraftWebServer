using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValtercraftWebServer.Services;
using ValtercraftWebServer.Data;
using ValtercraftWebServer.Models;

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
    }
}
