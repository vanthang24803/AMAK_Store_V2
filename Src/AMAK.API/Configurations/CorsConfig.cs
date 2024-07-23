namespace AMAK.API.Configurations {
    public static class CorsConfig {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services) {

            var Default = "_myAllowSpecificOrigins";

            services.AddCors(options => {
                options.AddPolicy(name: Default, builder => {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }
    }
}