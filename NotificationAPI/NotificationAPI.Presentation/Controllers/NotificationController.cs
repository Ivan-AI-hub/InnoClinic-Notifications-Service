using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Application.Abstraction;

namespace NotificationAPI.Presentation.Controllers
{
    [ApiController]
    [Route("/Notifications/")]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPut("SendAll")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendAllEmails(CancellationToken cancellationToken)
        {
            await _notificationService.SendAllNotifications(cancellationToken);
            return Ok();
        }
    }
}
