using System.Net;
using Newtonsoft.Json;

namespace AMAK.Application.Common.Helpers {
    public class Response<T>(HttpStatusCode status, T result) {
        [JsonProperty("status")]
        public HttpStatusCode Status { get; set; } = status;

        [JsonProperty("result")]
        public T Result { get; set; } = result;
    }
}