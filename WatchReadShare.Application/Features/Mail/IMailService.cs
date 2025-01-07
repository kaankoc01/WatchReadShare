namespace WatchReadShare.Application.Features.Mail
{
    public interface IMailService
    {
        public Task<int> SendEmailAsync(string toEmail, string subject, string body);
    }
}
