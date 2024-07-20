using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Domain.Models;
using AutoMapper;

namespace AMAK.Application.Mapper {
    public class ViewModelToDomainMappingProfile : Profile {
        public ViewModelToDomainMappingProfile() {
            CreateMap<RegisterRequest, ApplicationUser>();
        }
    }
}