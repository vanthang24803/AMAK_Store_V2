using Newtonsoft.Json;

namespace AMAK.Application.Common.Helpers {
    public class PaginationResponse<T> where T : class {
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("totalPage")]
        public int TotalPage { get; set; }

        [JsonProperty("items")]
        public int Items { get; set; }

        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("result")]
        public required T Result { get; set; }
    }
}