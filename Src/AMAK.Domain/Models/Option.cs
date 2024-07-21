using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class Option : BaseEntity<Guid> {
        public string Name { get; set; } = null!;

        public int Sale { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public bool IsActive { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}