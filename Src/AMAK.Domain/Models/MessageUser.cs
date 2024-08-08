namespace AMAK.Domain.Models {
    public class MessageUser {
        public string UserId { get; set; } = null!;
        public Guid NonfictionId { get; set; }
        public bool IsSeen { get; set; } = false;
        public DateTime? SeenAt { get; set; }
        public bool IsOpened { get; set; } = false;
    }
}