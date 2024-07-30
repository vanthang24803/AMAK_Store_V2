
using AMAK.Application.Services.Me.Dtos;
using AMAK.Application.Services.Photo.Dtos;

namespace AMAK.Application.Services.Review.Dtos {
    public class ReviewResponse {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public int Star { get; set; }
        public List<PhotoResponse> Photos { get; set; } = null!;
        public ProfileResponse User { get; set; } = null!;
    };
}