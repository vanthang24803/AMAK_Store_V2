
namespace AMAK.Application.Mapper {
    public class MapperConfig {
        public static Type[] RegisterMappings() {
            return
            [
                typeof(DomainToViewModelMappingProfile),
            typeof(ViewModelToDomainMappingProfile),
        ];
        }
    }
}