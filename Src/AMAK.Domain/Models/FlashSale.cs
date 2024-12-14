using AMAK.Domain.Enums;

namespace AMAK.Domain.Models {
    public class FlashSale : BaseEntity<Guid> {
        public string Name { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public EFlashSale Status { get; set; }
        public List<Product> Products { get; set; } = [];
    }
}