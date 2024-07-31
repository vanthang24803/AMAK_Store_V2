using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Order.Dtos {
    public class OrderResponse {
        public Guid Id { get; set; }
        public string Customer { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string NumberPhone { get; set; } = null!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPayment Payment { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EOrderStatus Status { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; } = [];

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }


    public class OrderDetailResponse {
        public Guid ProductId { get; set; }
        public string Thumbnail { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public string OptionName { get; set; } = null!;
        public double Price { get; set; }
        public int Sale { get; set; }
    }
}