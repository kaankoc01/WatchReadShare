namespace WatchReadShare.Application
{
    public interface ITokenService
    {
        string GenerateJwtToken(string userId, string email, IEnumerable<string> roles);
        string GenerateRefreshToken();
    }
}
