using Microsoft.AspNetCore.Identity;
using WatchReadShare.Domain.Entities;

namespace WatchReadShare.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto);
        Task<TokenResponse?> LoginAsync(LoginDto loginDto);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken); // Refresh token endpoint'i için
        Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
        Task<AppUser?> GetUserByEmailAsync(string email);
        public Task<bool> ResendConfirmationEmailAsync(AppUser user);
        public Task<AppUser?> GetUserByIdAsync(string Id);
    }
}
