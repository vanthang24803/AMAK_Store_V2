namespace AMAK.Domain.Models {
    public class Notification : BaseEntity<Guid> {
        public string Content { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsGlobal { get; set; } = false;
        public List<ApplicationUser> Users { get; } = [];
    }
}