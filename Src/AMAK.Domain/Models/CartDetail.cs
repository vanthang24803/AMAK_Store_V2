using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class CartDetail {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long Quantity { get; set; }

        public Guid OptionId { get; set; }

        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; } = null!;
        public string CartId { get; set; } = null!;

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;

    }
}