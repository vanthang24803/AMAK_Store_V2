using AMAK.Application.Providers.Configuration.Dtos;


namespace AMAK.API.Extensions {
    public static class ProviderExtension {
        public static void AddProviders(this IServiceCollection services,
            IConfiguration configuration) {

            // TODO: Mail Settings
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            // TODO: Mail Settings
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySetting"));

            // TODO: Mono Settings
            services.Configure<MomoSettings>(configuration.GetSection("Momo"));
        }
    }
}