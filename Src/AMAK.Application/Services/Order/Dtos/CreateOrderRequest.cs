using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Order.Dtos {
    public class CreateOrderRequest {
        public Guid Id { get; set; }
        public string Customer { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NumberPhone { get; set; } = null!;
        public string Address { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPayment Payment { get; set; }
        public List<OrderDetail> Products { get; set; } = [];
        public int Quantity { get; set; }
        public string? Voucher { get; set; }
        public double TotalPrice { get; set; }
    }

    public class OrderDetail {
        public Guid OptionId { get; set; }
        public Guid ProductId {get; set; }
        public string ProductName { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string OptionName { get; set; } = null!;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Sale { get; set; }
    }
}