using System.Net;
using Newtonsoft.Json;

namespace AMAK.Application.Common.Helpers {
    public class Response<T> {
        [JsonProperty("code")]
        public HttpStatusCode Code { get; set; }
        [JsonProperty("status")]
        public string Status { get; private set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;

        [JsonProperty("result")]
        public T Result { get; set; }

        public Response(HttpStatusCode code, T result) {
            Code = code;
            Result = result;
            Status = (code == HttpStatusCode.OK || code == HttpStatusCode.Created) ? "success" : "failure";
        }
    }
}
