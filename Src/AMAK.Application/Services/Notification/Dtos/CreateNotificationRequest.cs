using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Notification.Dtos {
    public class CreateGlobalNotificationRequest {
        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;
    }

}