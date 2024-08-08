using AMAK.Application.Services.Notification;
using AMAK.Application.Services.Notification.Dtos;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMAK.API.Controllers.v1 {
    [ApiVersion(1)]
    [Authorize]
    public class NotificationsController : BaseController {

        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService) {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("Global")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateGlobal([FromBody] CreateGlobalNotificationRequest request) {
            return StatusCode(StatusCodes.Status201Created, await _notificationService.CreateGlobalNotification(request));
        }

        [HttpGet]
        [Route("Me")]
        public async Task<IActionResult> GetNotification() {
            return Ok(await _notificationService.GetAllNotificationForAccount(User));
        }

        [HttpPost]
        [Route("Open")]

        public async Task<IActionResult> OpendNotification() {
            return Ok(await _notificationService.OpenAllNotification(User));
        }

        [HttpPost]
        [Route("Seen")]

        public async Task<IActionResult> SeenAllNotification() {
            return Ok(await _notificationService.SeenAllNotification(User));
        }
    }
}