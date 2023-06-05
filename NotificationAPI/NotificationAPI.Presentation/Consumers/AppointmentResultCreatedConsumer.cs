using MassTransit;
using NotificationAPI.Application.Abstraction;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class AppointmentResultCreatedConsumer : IConsumer<AppointmentResultCreated>
    {
        private readonly IEmailSendingService _emailSendingService;
        private readonly IUserService _userService;

        public AppointmentResultCreatedConsumer(IEmailSendingService emailSendingService, IUserService userService)
        {
            _emailSendingService = emailSendingService;
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<AppointmentResultCreated> context)
        {
            var message = context.Message;
            var user = await _userService.GetByIdAsync(message.PatientId);
            await _emailSendingService.SendAppointmentResultMessage(user.Email, message.Complaints, message.Conclusion, message.Recomendations);
        }
    }
}
