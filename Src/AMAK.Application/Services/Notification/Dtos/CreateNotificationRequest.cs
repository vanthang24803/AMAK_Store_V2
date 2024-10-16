using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Notification.Dtos {
    public class CreateGlobalNotificationRequest {
        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;

        public bool IsGlobal { get; set; }

        public List<UserNotification> Users { get; set; } = [];


    }

    public class UserNotification {
        public string? Id { get; set; }
    }

}