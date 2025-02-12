using System.ComponentModel.DataAnnotations.Schema;
using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class RefundTimeline {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ERefundTimeline Status { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}