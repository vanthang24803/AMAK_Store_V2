using AMAK.Application.Services.Address.Dtos;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Category.Dtos;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Application.Services.Product.Dtos;
using AMAK.Domain.Models;
using AutoMapper;

namespace AMAK.Application.Mapper {
    public class DomainToViewModelMappingProfile : Profile {
        public DomainToViewModelMappingProfile() {
            CreateMap<ApplicationUser, RegisterResponse>();
            CreateMap<ApplicationUser, ProfileResponse>();
            CreateMap<Address, AddressResponse>();
            CreateMap<Domain.Models.Category, CategoryResponse>();
            CreateMap<Product, ProductResponse>();
            CreateMap<Product, ProductDetailResponse>();
        }
    }
}