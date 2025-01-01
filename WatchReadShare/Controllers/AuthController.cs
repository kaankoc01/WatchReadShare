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
            var token = await authService.RegisterAsync(registerDto);
            if (string.IsNullOrEmpty(token)) return BadRequest("Kayıt işlemi başarısız!");

            return Ok(new { Token = token });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await authService.LoginAsync(loginDto);
            if (string.IsNullOrEmpty(token)) return Unauthorized("Geçersiz kullanıcı adı veya şifre!");

            return Ok(new { Token = token });
        }
    }
}
