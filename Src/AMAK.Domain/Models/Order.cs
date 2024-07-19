using System.ComponentModel.DataAnnotations.Schema;
using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class Order : BaseEntity<Guid> {
        public string? Email { get; set; }
        public string? Customer { get; set; }
        public string? Address { get; set; }

        public EPayment Payment { get; set; }

        public bool Shipping { get; set; }

        public EOrderStatus Status { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public List<Product> Products { get; } = [];
    }
}