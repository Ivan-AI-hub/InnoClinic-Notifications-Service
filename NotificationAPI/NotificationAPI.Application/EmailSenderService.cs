using NotificationAPI.Application.Abstraction;

namespace NotificationAPI.Application
{
    public class EmailSenderService : IEmailSendingService
    {
        private readonly IEmailService _emailService;

        public EmailSenderService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendAppointmentNotificationMessage(string to, string patientFullName, string doctorFullName, string serviceName, DateTime date)
        {
            await _emailService.SendScheduledEmail(to,
                              date - TimeSpan.FromDays(1),
                              "Appointment soon",
                              $"Patient: {patientFullName}\n" +
                              $"Doctor: {doctorFullName}\n" +
                              $"Date: {date.ToShortDateString()}\n" +
                              $"Time: {date.TimeOfDay}\n" +
                              $"Service: {serviceName}\n",
                              "");
        }

        public async Task SendAppointmentResultMessage(string to, string complaints, string conclusion, string recomendations)
        {
            await _emailService.SendAsync(to,
                              "Appointment result!!!!!!",
                              $"Complaints: {complaints}\n" +
                              $"Conclusion: {conclusion}\n" +
                              $"Recomendations: {recomendations}\n",
                              "");
        }

        public async Task SendVerifyEmailMessage(string to, string confirmEmailUrl)
        {
            await _emailService.SendAsync(to,
                              "Confirm email address",
                              "",
                              $"<a href='{confirmEmailUrl}'>Тыкни чтобы подтвердить</a>");
        }
    }
}
