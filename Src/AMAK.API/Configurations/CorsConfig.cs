using AMAK.Application.Providers.WS;

namespace AMAK.API.Configurations {
    public static class CorsConfig {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services) {

            var Default = "_myAllowSpecificOrigins";

            services.AddCors(options => {
                options.AddPolicy(name: Default, builder => {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            return services;
        }

        public static WebApplication UseCustomCors(this WebApplication app) {
            app.UseCors("_myAllowSpecificOrigins");
            return app;
        }

    }
}