using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentNotifications()
        {
            var notifications = await _notificationService.GetRecentNotificationsAsync();
            return Ok(notifications);
        }
    }
}
