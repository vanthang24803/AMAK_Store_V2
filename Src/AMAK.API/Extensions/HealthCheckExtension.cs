namespace AMAK.API.Extensions {
    public static class HealthCheckExtension {
        public static IServiceCollection AddCustomizedHealthCheck(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {

            if (env.IsProduction() || env.IsStaging()) {
                services.AddHealthChecks()
                        .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!);
            }

            return services;
        }
    }
}