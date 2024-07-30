using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Services.Review.Dtos {
    public class ReviewRequest {
        public int Star { get; set; }
        [Required]
        [MaxLength(256)]
        public string Content { get; set; } = null!;
    }
}