using AMAK.Application.Providers.Configuration.Dtos;
using AMAK.Application.Providers.Momo;
using AMAK.Application.Providers.Cloudinary;

namespace AMAK.API.Extensions {
    public static class ProviderExtension {
        public static IServiceCollection AddProviders(this IServiceCollection services, IConfiguration configuration) {

            // TODO: Mail Settings
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            // TODO: Mail Settings
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySetting"));

            // TODO: Mono Settings
            services.Configure<MomoSettings>(configuration.GetSection("Momo"));

            return services;
        }
    }
}