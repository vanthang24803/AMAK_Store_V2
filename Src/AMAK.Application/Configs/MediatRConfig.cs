using Microsoft.Extensions.DependencyInjection;

namespace AMAK.Application.Configs {
    public static class MediatRConfig {
        public static IServiceCollection AddCQRS(this IServiceCollection services) {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(MediatRConfig).Assembly));

            return services;
        }
    }
}