using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Photo.Dtos;

namespace AMAK.Application.Services.Product.Common {
    public record ProductResponse(
        Guid Id,
        string Name,
        string? Brand,
        List<CategoryResponse> Categories,
        List<OptionResponse> Options,
        List<PhotoResponse> Photos,
        DateTime CreateAt);
}