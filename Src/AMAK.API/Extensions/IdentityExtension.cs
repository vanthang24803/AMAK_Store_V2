using AMAK.Domain.Models;
using AMAK.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace AMAK.API.Extensions {
    public static class IdentityExtension {
        public static IServiceCollection CustomIdentity(this IServiceCollection services) {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}