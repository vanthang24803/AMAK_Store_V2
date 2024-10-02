namespace AMAK.Domain.Models {
    public class Voucher : BaseEntity<Guid> {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int Quantity { get; set; }
        public bool IsExpire { get; set; }
        public int Day { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int Discount { get; set; }
    }
}