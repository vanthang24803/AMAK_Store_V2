
using System.ComponentModel.DataAnnotations;

namespace AMAK.Domain.Models {
    public class Cart {
        [Key]
        public string Id { get; set; } = null!;
        public ICollection<CartDetail> Details { get; set; } = [];
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}