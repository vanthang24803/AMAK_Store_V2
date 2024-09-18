using System.ComponentModel.DataAnnotations.Schema;
using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class OrderStatus {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EOrderStatus Status { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}