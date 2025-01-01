using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchReadShare.Application.Features.Auth;

namespace WatchReadShare.API.Controllers
{
    public class AuthController(IAuthService authService) : CustomBaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await authService.RegisterAsync(registerDto);
            if (result == null)
            {
                return BadRequest("Kullanıcı kaydı başarısız.");
            }

            return Ok(new { Token = result });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var tokenResponse = await authService.LoginAsync(loginDto);
            return Ok(tokenResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var tokenResponse = await authService.RefreshTokenAsync(refreshToken);
            return Ok(tokenResponse);
        }
    }
}
