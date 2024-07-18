using Newtonsoft.Json;

namespace AMAK.Domain.Common.Helpers {
    public class ApiError {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("timestamp")]
        public DateTime Timestamp = DateTime.Now;

        public ApiError() {
        }
    }
}