using AMAK.Application.Services.Photo.Dtos;

namespace AMAK.Application.Services.Review.Dtos {
    public class ReviewResponse {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public float Star { get; set; }
        public List<PhotoResponse> Photos { get; set; } = null!;
        public ProfileReviewResponse User { get; set; } = null!;
    };

    public class ProfileReviewResponse {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}