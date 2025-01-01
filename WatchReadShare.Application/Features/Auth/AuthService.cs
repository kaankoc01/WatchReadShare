using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WatchReadShare.Domain.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using Microsoft.AspNetCore.Identity.Data;

namespace WatchReadShare.Application.Features.Auth
{
    public class AuthService(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager, IConfiguration configuration,ITokenService tokenService) : IAuthService
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

        public async Task<TokenResponse> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new Exception("Email veya şifre hatalı.");
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!signInResult.Succeeded)
            {
                throw new Exception("Email veya şifre hatalı.");
            }

            // Kullanıcı rolleri alınır
            var userRoles = await userManager.GetRolesAsync(user);

            // Access token ve refresh token oluşturulur
            var accessToken = tokenService.GenerateJwtToken(user.Id.ToString(), user.Email, userRoles);
            var refreshToken = tokenService.GenerateRefreshToken();

            // Refresh token'ı kullanıcıya kaydet
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7); // Refresh token 7 gün geçerli olacak
            await userManager.UpdateAsync(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            // Kullanıcıyı refresh token üzerinden bul
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpiration <= DateTime.UtcNow)
            {
                throw new Exception("Geçersiz veya süresi dolmuş refresh token.");
            }

            // Yeni token'lar oluştur
            var userRoles = await userManager.GetRolesAsync(user);
            var accessToken = tokenService.GenerateJwtToken(user.Id.ToString(), user.Email, userRoles);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            // Yeni refresh token'ı kaydet
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
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

