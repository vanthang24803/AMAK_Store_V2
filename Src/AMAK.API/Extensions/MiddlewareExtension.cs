
using AMAK.API.Middlewares;

namespace AMAK.API.Extensions {
    public static class MiddlewareExtension {

        public static WebApplication MiddlewareCustom(this WebApplication app) {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}