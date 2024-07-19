using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class Photo : BaseEntity<Guid> {

        public required string Url { get; set; }
        public string? PublicId { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

    }
}