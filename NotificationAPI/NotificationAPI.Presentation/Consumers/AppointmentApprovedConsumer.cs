using MassTransit;
using NotificationAPI.Application.Abstraction;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class AppointmentApprovedConsumer : IConsumer<AppointmentApproved>
    {
        private readonly IEmailSendingService _emailSendingService;
        private readonly IUserService _userService;

        public AppointmentApprovedConsumer(IEmailSendingService emailSendingService, IUserService userService)
        {
            _emailSendingService = emailSendingService;
            _userService = userService;
        }
        public async Task Consume(ConsumeContext<AppointmentApproved> context)
        {
            var message = context.Message;
            var user = await _userService.GetByIdAsync(message.PatientId);
            await _emailSendingService.SendAppointmentNotificationMessage(user.Email, message.PatientFullName, message.DoctorFullName,
                                                                          message.ServiceName, message.Date);
        }
    }
}
