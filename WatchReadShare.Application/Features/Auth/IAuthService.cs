namespace WatchReadShare.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterDto registerDto);
        Task<string?> LoginAsync(LoginDto loginDto);
    }
}
