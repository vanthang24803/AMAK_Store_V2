using Newtonsoft.Json;

namespace AMAK.Application.Common.Helpers {
    public class ApiError {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = null!;

        [JsonProperty("timestamp")]
        public DateTime Timestamp = DateTime.Now;
    }
}