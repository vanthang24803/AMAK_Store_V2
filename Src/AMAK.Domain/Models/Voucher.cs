namespace AMAK.Domain.Models {
    public class Voucher : BaseEntity<Guid> {
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public int Quantity { get; set; }

        public bool IsExpire { get; set; }

        public int Day { get; set; }

        public int Discount { get; set; }

        public DateTime ShelfLife { get; set; }
    }
}