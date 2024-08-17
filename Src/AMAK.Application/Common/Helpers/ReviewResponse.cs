using System.Text.Json.Serialization;

namespace AMAK.Application.Common.Helpers {
    public class ListReviewResponse<T> : PaginationResponse<T> where T : class {
        [JsonPropertyName("average_star")]
        public float AverageStar { get; set; }
    }
}