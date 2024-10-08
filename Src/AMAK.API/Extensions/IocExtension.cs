using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication;
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
using AMAK.Application.Providers.ElasticSearch;
using AMAK.Application.Providers.Google;
using AMAK.Application.Services.Gmail;
using AMAK.Application.Services.Cart;
using AMAK.Application.Services.Trash;
using AMAK.Application.Services.Tickets;

namespace AMAK.API.Common.Extensions {
    public static class IocExtension {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            // TODO: Authentication
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IAuthService, AuthService>();

            // TODO:Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>));


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
            services.AddScoped<IGmailStoreService, GmailStoreService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ITrashService, TrashService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ITicketService, TicketService>();

            services.AddSingleton<IGoogleService, GoogleService>();
            services.AddSingleton<IUploadService, UploadService>();
            services.AddSingleton<Application.Providers.Mail.IMailService, Application.Providers.Mail.MailService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}