
namespace AMAK.Application.Services.Analytics.Dtos {
    public class AnalyticsUserResponse {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public string Rank { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}