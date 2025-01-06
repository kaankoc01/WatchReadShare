namespace WatchReadShare.Application.Features.Auth
{
    public interface IMailService
    {
        public Task<int> SendEmailAsync(string toEmail, string subject, string body);
    }
}
