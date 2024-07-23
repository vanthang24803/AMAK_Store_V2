using AMAK.Application.Services.Address.Dtos;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Photo.Dtos;
using AMAK.Application.Services.Product.Common;
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
            CreateMap<Option, OptionResponse>();
            CreateMap<Photo, PhotoResponse>();
        }
    }
}