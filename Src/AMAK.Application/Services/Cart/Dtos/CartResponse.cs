
namespace AMAK.Application.Services.Cart.Dtos {
    public class CartResponse {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public Guid OptionId { get; set; }
        public string OptionName { get; set; } = null!;
        public int Sale { get; set; }
        public long Quantity { get; set; }
        public double Price { get; set; }
    }
}