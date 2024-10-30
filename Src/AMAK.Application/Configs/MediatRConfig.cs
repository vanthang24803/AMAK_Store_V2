using Microsoft.Extensions.DependencyInjection;

namespace AMAK.Application.Configs {
    public static class MediaTrConfig {
        public static void AddCqrs(this IServiceCollection services) {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(MediaTrConfig).Assembly));
        }
    }
}