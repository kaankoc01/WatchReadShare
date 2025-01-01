using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WatchReadShare.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WatchReadShare.Application.Features.Auth
{
    public class AuthService(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager, IConfiguration configuration) : IAuthService
    {
        public async Task<string?> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return GenerateToken(user);
            }

            return null; // Register başarısızsa null dön.
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Email);
            if (user is null) return null;

            var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                return GenerateToken(user);
            }

            return null;
        }
        private string GenerateToken(AppUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

