using Asp.Versioning;

namespace AMAK.API.Extensions {
    public static class VersionExtension {
        public static IServiceCollection AddAPIVersion(this IServiceCollection services) {

            services.AddApiVersioning(options => {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader()
                );
            }).AddApiExplorer(opts => {
                opts.GroupNameFormat = "'v'V";
                opts.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}