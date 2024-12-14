using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.FlashSale.Dtos {
    public class CreateFlashSaleRequest {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartAt { get; set; }

        [Required]
        public DateTime EndAt { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EFlashSale Status { get; set; }

        public List<Sale> Sales { get; set; } = [];
    }

    public class Sale {
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }
    }
}