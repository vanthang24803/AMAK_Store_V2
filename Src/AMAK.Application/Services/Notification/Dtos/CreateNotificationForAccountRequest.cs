using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Notification.Dtos {
    public class CreateNotificationForAccountRequest {
        [Required]
        public string Url { get; set; } = null!;
        [Required]
        public string Content { get; set; } = null!;
        public bool IsGlobal { get; set; }
        public string UserId { get; set; } = null!;
    }
}