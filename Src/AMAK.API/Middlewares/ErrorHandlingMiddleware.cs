using System.Net;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using Newtonsoft.Json;

namespace AMAK.API.Middlewares {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (BadRequestException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            } catch (UnauthorizedException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
            } catch (NotFoundException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            } catch (ForbiddenException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Forbidden);
            } catch (InternalServerErrorException ex) {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var errorResponse = new ApiError {
                Status = (int)statusCode,
                Message = exception.Message,
                Timestamp = DateTime.Now
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}