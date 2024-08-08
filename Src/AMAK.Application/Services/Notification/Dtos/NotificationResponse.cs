namespace AMAK.Application.Services.Notification.Dtos {
    public class NotificationResponse {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsSeen { get; set; }
        public string Url { get; set; } = null!;
        public DateTime? SeenAt { get; set; }
        public bool IsOpened { get; set; }
        public DateTime CreateAt { get; set; }
    }

}