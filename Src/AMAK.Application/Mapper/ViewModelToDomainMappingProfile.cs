using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Domain.Models;
using AutoMapper;

namespace AMAK.Application.Mapper {
    public class ViewModelToDomainMappingProfile : Profile {
        public ViewModelToDomainMappingProfile() {
            CreateMap<RegisterRequest, ApplicationUser>();
            CreateMap<UpdateProfileRequest, ApplicationUser>()
            .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}