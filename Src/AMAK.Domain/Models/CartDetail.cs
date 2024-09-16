using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class CartDetail {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string OptionId { get; set; } = null!;
        public string OptionName { get; set; } = null!;
        public int Sale { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string CartId { get; set; } = null!;

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; } = null!;

    }
}