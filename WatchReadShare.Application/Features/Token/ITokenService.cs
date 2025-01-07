namespace WatchReadShare.Application.Features.Token
{
    public interface ITokenService
    {
        string GenerateJwtToken(string userId, string email, IEnumerable<string> roles);
        string GenerateRefreshToken();
    }
}
