namespace AMAK.API.Configurations {
    public static class EnvConfig {
        public static IServiceCollection AddEnvConfig(this IServiceCollection services) {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .Build();

            services.AddSingleton<IConfiguration>(config);

            return services;
        }
    }
}