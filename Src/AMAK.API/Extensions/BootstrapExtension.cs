using AMAK.API.Configurations;
using AMAK.Application.Configs;
using AMAK.Application.Providers.RabbitMq;

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
            services.AddCorsConfig();

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
            services.AddApiVersion();

            // TODO: Providers
            services.AddProviders(builder.Configuration);

            // TODO: CQRS
            services.AddCqrs();

            // TODO: Redis
            services.AddRedisConfig(builder.Configuration);

            // TODO: Elastic Search
            services.AddElasticSearch(builder.Configuration);

            // TODO: Logger
            builder.Host.AddLoggerConfig();

            // TODO: WS
            services.AddWsConfig();

            // TODO: Queue
            services.AddScoped<IRabbitProducer, RabbitProducer>();
            services.AddHostedService<RabbitConsumer>();


            return services;
        }


        public static WebApplication StartUp(this WebApplication app) {
            app.UseHealthCheck();
            app.MapControllers();
            app.AddMiddleware();
            app.UseAPIDocs();
            app.UseWs();
            app.UseCustomCors();
            app.UseSerilogRequestLogging();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();

            return app;
        }
    }
}