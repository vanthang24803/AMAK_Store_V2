using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Domain.Models;
using AutoMapper;

namespace AMAK.Application.Mapper {
    public class DomainToViewModelMappingProfile : Profile {
        public DomainToViewModelMappingProfile() {
            CreateMap<ApplicationUser, RegisterResponse>()
            ;
        }
    }
}