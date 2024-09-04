
namespace AMAK.Domain.Models {
    public class Chat : BaseEntity<Guid> {
        public string Content { get; set; } = null!;
        public string FromUserId { get; set; } = null!;
        public string ToUserId { get; set; } = null!;
    }
}