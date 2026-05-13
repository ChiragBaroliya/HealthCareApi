using Microsoft.AspNetCore.Mvc;
using HealthCare.Application.Services;
using HealthCare.Application.DTOs;
using Asp.Versioning;

namespace HealthCare.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var res = await _userService.LoginAsync(request);
            if (!res.Success) return Unauthorized(res);
            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var res = await _userService.RegisterAsync(request);
            if (!res.Success) return BadRequest(res);
            return Ok(res);
        }
    }
}
