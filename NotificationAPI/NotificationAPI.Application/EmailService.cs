using Microsoft.Extensions.Options;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Application.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NotificationAPI.Application
{
    public class EmailService : IEmailService
    {
        private ISendGridClient _sendGridClient;
        private EmailAddress _from;

        public EmailService(IOptions<EmailSettings> settings)
        {
            var _settings = settings.Value;
            _sendGridClient = new SendGridClient(_settings.SendGridAPIKey);
            _from = new EmailAddress(_settings.Email, _settings.Name);
        }

        public async Task SendAsync(string toEmail, string subject, string? plainTextContent, string? htmlContent, CancellationToken cancellationToken = default)
        {
            var to = new EmailAddress(toEmail);
            var message = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);
            await _sendGridClient.SendEmailAsync(message, cancellationToken);
        }

        public async Task SendScheduledEmail(string toEmail, DateTime sendAt,
            string subject, string? plainTextContent, string? htmlContent, CancellationToken cancellationToken = default)
        {
            var to = new EmailAddress(toEmail);
            var message = MailHelper.CreateSingleEmail(_from, to, subject, plainTextContent, htmlContent);
            message.SendAt = new DateTimeOffset(sendAt).ToUnixTimeSeconds();

            await _sendGridClient.SendEmailAsync(message, cancellationToken);
        }

    }
}
