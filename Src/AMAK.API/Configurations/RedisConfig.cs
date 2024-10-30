
namespace AMAK.API.Configurations {
    public static class RedisConfig {
        public static void AddRedisConfig(this IServiceCollection services,
            IConfiguration configuration) {

            services.AddStackExchangeRedisCache(options => {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
        }
    }
}