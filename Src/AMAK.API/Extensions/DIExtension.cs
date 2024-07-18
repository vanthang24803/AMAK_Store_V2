using AMAK.Application.Interfaces;
using AMAK.Application.Services;
using AMAK.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AMAK.API.Extensions {
    public static class DIExtension {
        public static IServiceCollection AddDIScope(this IServiceCollection services) {
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection AddDISingleton(this IServiceCollection services) {

            return services;
        }
    }
}