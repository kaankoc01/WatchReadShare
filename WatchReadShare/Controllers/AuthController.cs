using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmailDto)
        {
            var result = await authService.ConfirmEmailAsync(confirmEmailDto);
            if (result == null)
            {
                return BadRequest("Email Doğrulanmamış.");
            }

            return Ok("Email başarıyla Doğrulandı");
        }
    


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await authService.GetUserByEmailAsync(email);
            if (user == null)
                return BadRequest("Kullanıcı bulunamadı!");

            var result = await authService.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return BadRequest("Doğrulama başarısız.");

            return Ok("Email başarıyla doğrulandı!");
        }

        
       [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendEmailDto resendEmailDto)
        {
            var user = await authService.GetUserByEmailAsync(resendEmailDto.Email);
            if (user == null)
                return NotFound(new { Message = "Kullanıcı bulunamadı." });

            var result = await authService.ResendConfirmationEmailAsync(user);
            if (result)
                return Ok(new { Message = "Doğrulama emaili başarıyla gönderildi." });

            return BadRequest(new { Message = "Email zaten doğrulanmış." });
        }


        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("Geçersiz parametreler.");
            }

            var user = await authService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var result = await authService.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Email doğrulama başarılı!");
            }

            return BadRequest("Email doğrulama başarısız.");
        }

    }

}