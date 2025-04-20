using AMAK.API.Middlewares;

namespace AMAK.API.Extensions {
    public static class MiddlewareExtension {
        public static void AddMiddleware(this WebApplication app) {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            // app.UseMiddleware<InterceptorMiddleware>();
        }
    }
}