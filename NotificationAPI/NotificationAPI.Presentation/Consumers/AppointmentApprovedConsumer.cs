using MassTransit;
using NotificationAPI.Application.Abstraction;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class AppointmentApprovedConsumer : IConsumer<AppointmentApproved>
    {
        private readonly IEmailSendingService _emailSendingService;
        private readonly IPatientService _patientService;

        public AppointmentApprovedConsumer(IEmailSendingService emailSendingService, IPatientService patientService)
        {
            _emailSendingService = emailSendingService;
            _patientService = patientService;
        }
        public async Task Consume(ConsumeContext<AppointmentApproved> context)
        {
            var message = context.Message;
            var patient = await _patientService.GetByIdAsync(message.PatientId);
            await _emailSendingService.SendAppointmentNotificationMessage(patient.Email, message.PatientFullName, message.DoctorFullName,
                                                                          message.ServiceName, message.Date);
        }
    }
}
