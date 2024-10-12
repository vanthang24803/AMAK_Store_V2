using AMAK.Application.Services.Address.Dtos;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Billboard.Dtos;
using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Photo.Dtos;
using AMAK.Application.Services.Product.Common;
using AMAK.Application.Services.Prompt.Dtos;
using AMAK.Application.Services.Review.Dtos;
using AMAK.Application.Services.Tickets.Dtos;
using AMAK.Domain.Models;
using AutoMapper;

namespace AMAK.Application.Mapper {
    public class DomainToViewModelMappingProfile : Profile {
        public DomainToViewModelMappingProfile() {
            CreateMap<ApplicationUser, RegisterResponse>();
            CreateMap<ApplicationUser, ProfileResponse>();
            CreateMap<ApplicationUser, ProfileReviewResponse>();
            CreateMap<Address, AddressResponse>();
            CreateMap<Domain.Models.Category, CategoryResponse>();
            CreateMap<Product, ProductResponse>();
            CreateMap<Product, ProductDetailResponse>();
            CreateMap<Option, OptionResponse>();
            CreateMap<Photo, PhotoResponse>();
            CreateMap<Billboard, BillboardResponse>();
            CreateMap<Review, ReviewResponse>();
            CreateMap<Prompt, PromptResponse>();
            CreateMap<Voucher, TicketResponse>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartAt))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndAt));
        }
    }
}