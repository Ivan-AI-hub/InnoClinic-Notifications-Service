namespace NotificationAPI.Application.Abstraction
{
    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string? plainTextContent, string? htmlContent, CancellationToken cancellationToken = default);
        Task SendScheduledEmail(string toEmail, DateTime sendAt, string subject, string? plainTextContent, string? htmlContent, CancellationToken cancellationToken = default);
    }
}