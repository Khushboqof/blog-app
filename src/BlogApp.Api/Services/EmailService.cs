using BlogApp.Api.Inerfaces.Services;
using BlogApp.Api.ViewModels.Users;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BlogApp.Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfigurationSection _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration.GetSection("Email");
        }

        public async Task SendAsync(EmailMesage emailMesage)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailAddress"]));
            email.To.Add(MailboxAddress.Parse(emailMesage.To));
            email.Subject = emailMesage.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailMesage.Body.ToString() };

            var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Host"], 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config["EmailAddress"], _config["Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
