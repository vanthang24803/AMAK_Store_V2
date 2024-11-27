namespace AMAK.API.Configurations {
    public static class CorsConfig {
        public static void AddCorsConfig(this IServiceCollection services) {

            const string cors = "_myAllowSpecificOrigins";

            services.AddCors(options => {
                options.AddPolicy(name: cors, builder => {
                    builder.WithOrigins(
                        "https://amak-client.vercel.app",
                        "http://localhost:3000",
                        "https://amak-client-poryzoztz-vanthang24803s-projects.vercel.app",
                        "https://zc2l5h18-3000.asse.devtunnels.ms"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
        }

        public static void UseCustomCors(this WebApplication app) {
            app.UseCors("_myAllowSpecificOrigins");
        }
    }
}
