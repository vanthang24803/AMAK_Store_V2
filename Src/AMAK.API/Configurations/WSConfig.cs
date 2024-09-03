using AMAK.Application.Providers.WS;

namespace AMAK.API.Configurations {
    public static class WSConfig {
        public static IServiceCollection AddWSConfig(this IServiceCollection services) {
            services.AddSignalR().AddHubOptions<ChatHub>(options => {
                options.EnableDetailedErrors = true;
            });

            return services;
        }

        public static WebApplication UseWS(this WebApplication app) {
            app.MapHub<ChatHub>("chat");
            return app;
        }
    }
}