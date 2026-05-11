using Microsoft.AspNetCore.Mvc;
using HealthCare.Application.Services;
using HealthCare.Application.DTOs;

namespace HealthCare.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
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
    }
}
