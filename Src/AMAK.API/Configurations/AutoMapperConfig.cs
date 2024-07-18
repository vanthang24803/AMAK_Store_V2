using AMAK.Application.Mapper;

namespace AMAK.API.Configurations {
    public static class AutoMapperConfig {
        public static void AddAutoMapperConfig(this IServiceCollection services) {
            services.AddAutoMapper(MapperConfig.RegisterMappings());
        }
    }
}