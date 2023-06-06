using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Application.Settings;

namespace NotificationAPI.Application
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(string toEmail, string subject, string message, CancellationToken cancellationToken = default)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.EmailAddress));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
                await client.AuthenticateAsync(_settings.EmailAddress, _settings.Password, cancellationToken);
                await client.SendAsync(emailMessage, cancellationToken);

                await client.DisconnectAsync(true, cancellationToken);
            }
        }

        public async Task SendScheduledEmail(string toEmail, DateTime sendAt,
            string subject, string message, CancellationToken cancellationToken = default)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.EmailAddress));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            emailMessage.Date = sendAt;

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
                await client.AuthenticateAsync(_settings.EmailAddress, _settings.Password, cancellationToken);
                await client.SendAsync(emailMessage, cancellationToken);

                await client.DisconnectAsync(true, cancellationToken);
            }
        }

    }
}
