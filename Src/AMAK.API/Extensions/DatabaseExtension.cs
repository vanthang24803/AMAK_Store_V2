using AMAK.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AMAK.API.Extensions {
    public static class DatabaseExtension {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}