using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models {
    public class OrderRefund {
        [Key]
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public bool IsAccepted { get; set; } = false;
        public string Message { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime ConfirmAt { get; set; }
        public DateTime SuccessAt { get; set; }
        public Order Order { get; set; } = null!;
        public List<OrderDetail> Items { get; set; } = [];
        public List<RefundTimeline> Timelines { get; set; } = [];
    }
}