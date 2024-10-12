using AMAK.Application.Services.Address.Dtos;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Prompt.Dtos;
using AMAK.Application.Services.Tickets.Dtos;
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

            CreateMap<UpdateOrderByAccountRequest, Order>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<OptionRequest, Option>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<PromptRequest, Prompt>()
                  .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<TicketSchema, Voucher>()
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndAt, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.IsExpire, opt => opt.MapFrom(src => src.EndDate > DateTime.UtcNow))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => (src.EndDate - src.StartDate).Days));


            CreateMap<OptionProductUpdateRequest, Option>().ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}