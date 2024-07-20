using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication;
using AMAK.Application.Services.Category;
using AMAK.Application.Providers.Mail;
using AMAK.Domain.Models;
using AMAK.Infrastructure.Repository;
using AMAK.Infrastructure.Token;
using Microsoft.AspNetCore.Identity;

namespace AMAK.API.Common.Extensions {
    public static class IocExtension {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            // TODO: Authentication
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IAuthService, AuthService>();
            
            // TODO:Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // TODO: Service
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddSingleton<ITokenService, TokenService>();

            return services;
        }
    }
}