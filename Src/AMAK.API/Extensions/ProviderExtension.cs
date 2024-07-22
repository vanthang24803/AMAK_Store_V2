
using AMAK.Application.Providers.Mail;
using AMAK.Application.Providers.Upload;

namespace AMAK.API.Extensions {
    public static class ProviderExtension {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration) {

            // TODO: Mail Settings
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            // TODO: Mail Settings
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySetting"));

            return services;
        }
    }
}