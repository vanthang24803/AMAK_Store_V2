using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Analytics.Dtos {
    public class StatisticalResponse {
        public double TotalPrice { get; set; }
        public long TotalOrder { get; set; }
        public long TotalSold { get; set; }
        public List<StatisticalOrderResponse> Orders { get; set; } = [];

        [JsonPropertyName("_startAt")]
        public string StartAt { get; set; } = null!;

        [JsonPropertyName("_endAt")]
        public string EndAt { get; set; } = null!;

        [JsonPropertyName("_currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("_limit")]
        public int Limit { get; set; }

        [JsonPropertyName("_totalPage")]
        public int TotalPage { get; set; }

        [JsonPropertyName("_totalItems")]
        public int TotalItems { get; set; }
    }

    public class StatisticalOrderResponse {
        public Guid Id { get; set; }

        public string Customer { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool IsReviewed { get; set; }

        public string NumberPhone { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EPayment Payment { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EOrderStatus Status { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
