using System.Net;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AMAK.API.Middlewares {
    public class InterceptorMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<InterceptorMiddleware> _logger;

        public InterceptorMiddleware(
            RequestDelegate next,
            ILogger<InterceptorMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) {

            if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/scalar") || context.Request.Path.StartsWithSegments("/Gemini")) {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;

            try {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                if (IsJsonResponse(context) && IsSuccessStatusCode(context.Response.StatusCode)) {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var rawResponse = await new StreamReader(responseBody).ReadToEndAsync();

                    var jsonObject = JObject.Parse(rawResponse);

                    object finalResponse;
                    HttpStatusCode statusCode = (HttpStatusCode)context.Response.StatusCode;

                    if (jsonObject["code"] != null && jsonObject["status"] != null && jsonObject["result"] != null) {
#pragma warning disable CS8604 // Possible null reference argument.
                        finalResponse = new ConvertResponse<JToken>(
                            statusCode,
                            jsonObject["result"]
                        ) {
                            Metadata = new Metadata {
                                Path = context.Request.Path,
                                Method = context.Request.Method,
                                Version = "1.0",
                                Device = context.Request.Headers.UserAgent!,
                                RequestID = context.TraceIdentifier
                            }
                        };
#pragma warning restore CS8604 // Possible null reference argument.
                    } else {
#pragma warning disable CS8604 // Possible null reference argument.
                        finalResponse = new ConvertResponse<object>(
                            statusCode,
                            result: JsonConvert.DeserializeObject(rawResponse)
                        ) {
                            Metadata = new Metadata {
                                Path = context.Request.Path,
                                Method = context.Request.Method,
                                Version = "1.0",
                                Device = context.Request.Headers.UserAgent!,
                                RequestID = context.TraceIdentifier
                            }
                        };
#pragma warning restore CS8604 // Possible null reference argument.
                    }

                    var jsonResponse = JsonConvert.SerializeObject(finalResponse);
                    context.Response.Body = originalBodyStream;
                    context.Response.ContentLength = Encoding.UTF8.GetByteCount(jsonResponse);
                    await context.Response.WriteAsync(jsonResponse);
                } else {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            } finally {
                context.Response.Body = originalBodyStream;
            }
        }

        private static bool IsJsonResponse(HttpContext context) {
            return context.Response.ContentType?.Contains("application/json") == true;
        }

        private static bool IsSuccessStatusCode(int statusCode) {
            return statusCode >= 200 && statusCode < 300;
        }
    }

    public class ConvertResponse<T>(HttpStatusCode code, T result) {
        [JsonProperty("code")]
        public HttpStatusCode Code { get; set; } = code;

        [JsonProperty("status")]
        public string Status => Code is HttpStatusCode.OK or HttpStatusCode.Created ? "success" : "failure";

        [JsonProperty("result")]
        public T Data { get; set; } = result;

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; } = new Metadata();
    }

    public class Metadata {
        [JsonProperty("version")]
        public string Version { get; set; } = "1.0";

        [JsonProperty("path")]
        public string Path { get; set; } = null!;

        [JsonProperty("method")]
        public string Method { get; set; } = null!;

        [JsonProperty("device")]
        public string Device { get; set; } = null!;

        [JsonProperty("requestId")]
        public string RequestID { get; set; } = null!;

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; } = DateTime.UtcNow;
    }
}