
namespace NotificationAPI.Application.Abstraction
{
    public interface IEmailSendingService
    {
        public Task SendVerifyEmailMessage(string to, string confirmEmailUrl);
        public Task SendAppointmentResultMessage(string to, string complaints, string conclusion, string recomendations);
        public Task SendAppointmentNotificationMessage(string to, string patientFullName, string doctorFullName, string serviceName, DateTime date);

    }
}
