using Nest;

namespace AMAK.API.Configurations {
    public static class ElasticSearchConfig {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration) {

            var url = configuration["ElasticSearchConfig:Url"];

            var settings = new ConnectionSettings(new Uri(url!))
                .DefaultIndex("products")
                .PrettyJson()
                .RequestTimeout(TimeSpan.FromMinutes(2));

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
        }
    }
}