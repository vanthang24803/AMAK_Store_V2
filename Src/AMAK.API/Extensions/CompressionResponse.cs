using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace AMAK.API.Extensions {
    public static class CompressionResponse {
        public static IServiceCollection AddCompressionResponse(this IServiceCollection services) {
            services.AddResponseCompression(options => {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();

                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "application/json",
                    "application/javascript",
                    "text/css",
                    "text/html",
                    "image/svg+xml"
                });
            });

            services.Configure<BrotliCompressionProviderOptions>(options => {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<GzipCompressionProviderOptions>(options => {
                options.Level = CompressionLevel.Fastest;
            });

            return services;
        }
    }
}
