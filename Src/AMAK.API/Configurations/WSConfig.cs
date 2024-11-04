using AMAK.Application.Providers.WS;

namespace AMAK.API.Configurations {
    public static class WsConfig {
        public static void AddWsConfig(this IServiceCollection services) {
            services.AddSignalR().AddHubOptions<ChatHub>(options => {
                options.EnableDetailedErrors = true;
            });
        }

        public static void UseWs(this WebApplication app) {
            app.MapHub<ChatHub>("chat");
        }
    }
}