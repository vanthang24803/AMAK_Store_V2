using System.Net;
using AMAK.Application.Common.Exceptions;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace AMAK.API.Middlewares {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (BadRequestException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
                LogError(ex, "Bad Request");
            } catch (UnauthorizedException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
                LogError(ex, "Unauthorized");
            } catch (NotFoundException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
                LogError(ex, "Not Found");
            } catch (ForbiddenException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Forbidden);
                LogError(ex, "Forbidden");
            } catch (InternalServerErrorException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
                LogError(ex, "Internal Server Error");
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
                LogError(ex, "Unhandled Exception");
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new Error {
                Code = statusCode,
                Message = exception.Message,
                Metadata = new Metadata {
                    Path = context.Request.Path,
                    Method = context.Request.Method,
                    Version = "1.0",
                    Device = context.Request.Headers.UserAgent!,
                    RequestID = context.TraceIdentifier,
                }
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }

        private void LogError(Exception ex, string errorType) {
            _logger.LogError(ex, "{ErrorType}: {Message}\nStack Trace: {StackTrace}",
                errorType,
                ex.Message,
                ex.StackTrace);
            if (ex.InnerException != null) {
                _logger.LogError("Inner Exception: {InnerMessage}", ex.InnerException.Message);
            }
        }
    }

    public class Error {
        [JsonProperty("code")]
        public HttpStatusCode Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; } = "failure";

        [JsonProperty("message")]
        public string Message { get; set; } = null!;

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; } = new Metadata();
    }
}