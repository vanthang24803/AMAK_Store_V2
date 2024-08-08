using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Notification.Dtos {
    public class CreateGlobalNotificationRequest {
        [MaxLength(256)]
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
    }

}