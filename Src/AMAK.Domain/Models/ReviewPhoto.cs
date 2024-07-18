
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class ReviewPhoto : BaseEntity<Guid> {

        public required string Url { get; set; }
        public string? PublicId { get; set; }

        public Guid ReviewId { get; set; }

        [ForeignKey(nameof(ReviewId))]
        public Review Review { get; set; } = null!;
    }
}