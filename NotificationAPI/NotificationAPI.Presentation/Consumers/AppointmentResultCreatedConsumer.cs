using MassTransit;
using NotificationAPI.Application.Abstraction;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class AppointmentResultCreatedConsumer : IConsumer<AppointmentResultCreated>
    {
        private readonly IEmailSendingService _emailSendingService;
        private readonly IPatientService _patientService;

        public AppointmentResultCreatedConsumer(IEmailSendingService emailSendingService, IPatientService patientService)
        {
            _emailSendingService = emailSendingService;
            _patientService = patientService;
        }

        public async Task Consume(ConsumeContext<AppointmentResultCreated> context)
        {
            var message = context.Message;
            var patient = await _patientService.GetByIdAsync(message.PatientId);
            await _emailSendingService.SendAppointmentResultMessage(patient.Email, message.Complaints, message.Conclusion, message.Recomendations);
        }
    }
}
