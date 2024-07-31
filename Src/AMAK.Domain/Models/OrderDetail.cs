namespace AMAK.Domain.Models {
    public class OrderDetail {
        public Guid OrderId { get; set; }
        public Guid OptionId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string OptionName { get; set; } = null!;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Sale { get; set; }
    }
}