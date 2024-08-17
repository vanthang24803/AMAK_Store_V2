using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication;
using AMAK.Application.Providers.Mail;
using AMAK.Domain.Models;
using AMAK.Infrastructure.Repository;
using AMAK.Infrastructure.Token;
using Microsoft.AspNetCore.Identity;
using AMAK.Application.Services.Me;
using AMAK.Application.Providers.Upload;
using AMAK.Application.Services.Address;
using AMAK.Application.Services.Options;
using AMAK.Application.Services.Photo;
using AMAK.Application.Services.Billboard;
using AMAK.Application.Services.Review;
using AMAK.Application.Services.Notification;
using AMAK.Application.Providers.Momo;
using AMAK.Application.Providers.Cache;
using AMAK.Application.Services.Analytics;

namespace AMAK.API.Common.Extensions {
    public static class IocExtension {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            // TODO: Authentication
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IAuthService, AuthService>();

            // TODO:Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // TODO: Service
            services.AddScoped<IMeService, MeService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IOptionsService, OptionService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IBillboardService, BillboardService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAnalyticService, AnalyticService>();

            services.AddScoped<IMomoService, MomoService>();
            services.AddSingleton<IUploadService, UploadService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}