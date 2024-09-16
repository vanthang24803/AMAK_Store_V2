
namespace AMAK.Application.Services.Cart.Dtos {
    public class CartRequest {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string OptionId { get; set; } = null!;
        public string OptionName { get; set; } = null!;
        public int Sale { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}