namespace AMAK.API.Configurations {
    public static class CorsConfig {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration configuration) {

            var Default = "_myAllowSpecificOrigins";

            services.AddCors(options => {
                options.AddPolicy(name: Default, builder => {
                    builder.WithOrigins(
                        "https://amak-client.vercel.app",
                        "http://localhost:3000",
                        "https://amak-client-poryzoztz-vanthang24803s-projects.vercel.app"
                    )
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
