
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class Review : BaseEntity<Guid> {
        public string? Content { get; set; }

        public float Star { get; set; }

        public IEnumerable<ReviewPhoto> Photos { get; set; } = [];

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}