using System.ComponentModel.DataAnnotations.Schema;
using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class Order : BaseEntity<Guid> {
        public string Email { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public string Address { get; set; } = null!;

        public string? NumberPhone { get; set; }

        public EPayment Payment { get; set; }

        public bool Shipping { get; set; }

        public EOrderStatus Status { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

        public bool IsReviewed { get; set; } = false;

        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        public List<Option> Options { get; } = [];
    }
}