using System.Text.Json.Serialization;

namespace AMAK.Application.Common.Helpers {
    public class PaginationResponse<T> where T : class {
        [JsonPropertyName("code")]
        public int Code { get; init; } = 200;

        [JsonPropertyName("status")]
        public string Status { get; set; } = "success";

        [JsonPropertyName("result")]
        public required T Result { get; init; }

        [JsonPropertyName("_currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("_totalPage")]
        public int TotalPage { get; set; }

        [JsonPropertyName("_limit")]
        public int Items { get; set; }

        [JsonPropertyName("_totalItems")]
        public int TotalItems { get; set; }
    }
}