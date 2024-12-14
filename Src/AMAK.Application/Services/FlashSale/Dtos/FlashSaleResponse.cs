
using System.Text.Json.Serialization;
using AMAK.Application.Services.Product.Common;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.FlashSale.Dtos {
    public class FlashSaleResponse {
        public Guid Id;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EFlashSale Status { get; set; }
        public List<ProductResponse> Products { get; set; } = [];
    }
}