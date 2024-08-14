
namespace AMAK.API.Configurations {
    public static class RedisConfig {
        public static IServiceCollection AddRedisConfig(this IServiceCollection services, IConfiguration configuration) {

            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetConnectionString("Redis");
            });

            return services;
        }
    }
}