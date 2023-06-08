using NotificationAPI.Application.Abstraction;
using NotificationAPI.Domain;
using NotificationAPI.Domain.Interfaces;

namespace NotificationAPI.Application
{
    public class EmailSendingService : IEmailSendingService
    {
        private readonly IEmailService _emailService;
        private readonly IScheduledNotificationRepository _notificationRepository;

        public EmailSendingService(IEmailService emailService, IScheduledNotificationRepository notificationRepository)
        {
            _emailService = emailService;
            _notificationRepository = notificationRepository;
        }

        public async Task SendAppointmentNotificationMessage(string to, string patientFullName, string doctorFullName, string serviceName, DateTime date)
        {
            var notification = new ScheduledNotification(to,
                              (date - TimeSpan.FromDays(1)).ToUniversalTime(),
                              "Appointment soon",
                              $"Patient: {patientFullName}\n" +
                              $"Doctor: {doctorFullName}\n" +
                              $"Date: {date.ToShortDateString()}\n" +
                              $"Time: {date.TimeOfDay}\n" +
                              $"Service: {serviceName}\n");
            await _notificationRepository.CreateAsync(notification);
        }

        public async Task SendAppointmentResultMessage(string to, string complaints, string conclusion, string recomendations)
        {
            await _emailService.SendAsync(to,
                              "Appointment result!!!!!!",
                              $"Complaints: {complaints}\n" +
                              $"Conclusion: {conclusion}\n" +
                              $"Recomendations: {recomendations}\n");
        }

        public async Task SendVerifyEmailMessage(string to, string confirmEmailUrl)
        {
            await _emailService.SendAsync(to,
                              "Confirm email address",
                              $"<a href='{confirmEmailUrl}'>Тыкни чтобы подтвердить</a>");
        }
    }
}
