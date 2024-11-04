using System.Net;
using Newtonsoft.Json;

namespace AMAK.Application.Common.Helpers {
    public class Response<T>(HttpStatusCode code, T result)
    {
        [JsonProperty("code")]
        public HttpStatusCode Code { get; set; } = code;

        [JsonProperty("status")]
        public string Status { get; private set; } = code is HttpStatusCode.OK or HttpStatusCode.Created ? "success" : "failure";

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [JsonProperty("result")]
        public T Result { get; set; } = result;
    }
}
