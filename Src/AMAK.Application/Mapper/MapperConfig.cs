
namespace AMAK.Application.Mapper {
    public abstract class MapperConfig {
        public static Type[] RegisterMappings() {
            return
            [
                typeof(DomainToViewModelMappingProfile),
                typeof(ViewModelToDomainMappingProfile),
        ];
        }
    }
}