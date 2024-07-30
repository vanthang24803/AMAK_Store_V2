using AMAK.Application.Services.Categories.Common;
using AMAK.Application.Services.Options.Dtos;
using AMAK.Application.Services.Photo.Dtos;

namespace AMAK.Application.Services.Product.Common {

    public record class ProductDetailResponse(
        Guid Id,
        string Name,
        string Brand,
        string Thumbnail,
        long Sold,
        string? Introduction,
        string? Specifications,
        List<CategoryResponse> Categories,
        List<OptionResponse> Options,
        List<PhotoResponse> Photos,
        DateTime CreateAt,
        DateTime UpdateAt);
}