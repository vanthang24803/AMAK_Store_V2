using AMAK.Domain.Models;
using AMAK.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace AMAK.API.Configurations {
    public static class IdentityConfig {
        public static IServiceCollection AddIdentity(this IServiceCollection services) {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}