using AMAK.Application.Services.Address.Dtos;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Categories.Commands.Create;
using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Product.Common;
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

            CreateMap<CategoryRequest, Domain.Models.Category>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateProductRequest, Product>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<OptionRequest, Option>().ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<OptionProductUpdate, Option>().ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

        }
    }
}