using AMAK.API.Common.Extensions;
using AMAK.API.Configurations;
using AMAK.Application.Configs;
using Serilog;

namespace AMAK.API.Extensions {
    public static class BootstrapExtension {
        public static IServiceCollection AddServices(this IServiceCollection services, WebApplicationBuilder builder) {
            // TODO: Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfig();

            // TODO: Env
            services.AddEnvConfig();

            // TODO: Cors
            services.AddCorsConfig(builder.Configuration);

            // TODO: Controller
            services.AddControllers();

            // TODO: Database Connection
            services.AddCustomizedDatabase(builder.Configuration, builder.Environment);
            services.AddHealthChecks();

            // TODO: Auto Mapper
            services.AddAutoMapperConfig();

            // TODO: Identity
            services.AddIdentity();

            // TODO: JWT
            services.AddJwt(builder.Configuration);

            // TODO: Authorization
            services.AddAuthorization();

            // TODO: Inject Dependency
            services.AddApplication();

            // TODO: API Version
            services.AddAPIVersion();

            // TODO: Providers
            services.AddProviders(builder.Configuration);

            // TODO: CQRS
            services.AddCQRS();

            // TODO: Redis
            services.AddRedisConfig(builder.Configuration);

            // TODO: Elastic Search
            services.AddElasticSearch(builder.Configuration);

            // TODO: Logger
            builder.Host.AddLoggerConfig();

            // TODO: WS
            services.AddWSConfig();

            return services;
        }


        public static WebApplication StartUp(this WebApplication app) {
            app.AddMiddleware();
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseWS();
            app.UseCustomCors();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();

            return app;
        }
    }
}