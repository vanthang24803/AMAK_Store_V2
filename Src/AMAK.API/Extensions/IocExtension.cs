using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication;
using AMAK.Application.Services.Category;
using AMAK.Domain.Models;
using AMAK.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;

namespace AMAK.API.Common.Extensions {
    public static class IocExtension {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}