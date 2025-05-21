using Microsoft.AspNetCore.Mvc;
using PatternApplication.FactoryMethodPattern;

namespace DesignPattern.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult SendNotification([FromBody] string message, string type)
        {
            try
            {
                _notificationService.NotifyUser(message, type);
                return Ok($"Notification sent");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
