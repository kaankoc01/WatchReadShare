namespace WatchReadShare.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterDto registerDto);
        Task<TokenResponse?> LoginAsync(LoginDto loginDto);
        Task<TokenResponse> RefreshTokenAsync(string refreshToken); // Refresh token endpoint'i için
    }
}
