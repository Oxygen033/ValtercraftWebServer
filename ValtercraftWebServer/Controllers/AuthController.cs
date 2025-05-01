using Microsoft.AspNetCore.Mvc;
using ValtercraftWebServer.DTO;
using ValtercraftWebServer.Services;

namespace ValtercraftWebServer.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var token = await _authService.RegisterAsync(dto);
            if (token == null)
                return BadRequest("Username already exists");

            Response.Headers.Add("Authorization", $"Bearer {token}");

            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized();

            Response.Headers.Add("Authorization", $"Bearer {token}");

            return Ok(new { message = "Login successful" });
        }
    }
}
