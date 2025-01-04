namespace WatchReadShare.Application.Features.Auth
{
    public interface IMailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
