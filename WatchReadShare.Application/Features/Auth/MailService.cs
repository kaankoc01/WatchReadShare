using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace WatchReadShare.Application.Features.Auth
{
    public class MailService(IConfiguration configuration) : IMailService
    {
        public async Task<int> SendEmailAsync(string toEmail, string subject, string body)
        {

          //  6 haneli rastgele doğrulama kodu oluştur
            int verificationCode = new Random().Next(100000, 999999);

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(configuration["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            // Mail içeriği
            email.Body = new TextPart("plain")
            {
                Text = $"Doğrulama Kodunuz: {verificationCode}\n\nBu kodu güvenli bir şekilde saklayın. Başkalarıyla paylaşmayın."
            };

            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {

                await smtp.ConnectAsync(configuration["MailSettings:Host"], int.Parse(configuration["MailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(configuration["MailSettings:Mail"], configuration["MailSettings:Password"]);
                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }

            return verificationCode;


        }
    }
}
