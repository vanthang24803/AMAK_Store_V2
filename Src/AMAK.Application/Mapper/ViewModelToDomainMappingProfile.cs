using AMAK.Application.Services.Address.Dtos;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Category.Dtos;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Domain.Models;
using AutoMapper;

namespace AMAK.Application.Mapper {
    public class ViewModelToDomainMappingProfile : Profile {
        public ViewModelToDomainMappingProfile() {
            CreateMap<RegisterRequest, ApplicationUser>();

            CreateMap<UpdateProfileRequest, ApplicationUser>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<AddressRequest, Address>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CategoryRequest, Category>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}