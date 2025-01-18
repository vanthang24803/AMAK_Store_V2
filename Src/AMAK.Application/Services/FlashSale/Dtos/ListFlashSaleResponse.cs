
using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.FlashSale.Dtos {
    public class ListFlashSaleResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EFlashSale Status { get; set; }
        public long Products { get; set; }
    }
}