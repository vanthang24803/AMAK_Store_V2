using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Review.Dtos;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Review {
    public interface IReviewService {
        Task<PaginationResponse<List<ReviewResponse>>> GetAllAsync(Guid productId, BaseQuery query);

        Task<PaginationResponse<List<ReviewResponse>>> GetAsync(ClaimsPrincipal claims, BaseQuery query);

        Task<Response<ReviewResponse>> GetOneAsync(Guid id);

        Task<Response<string>> CreateAsync(ClaimsPrincipal claims, CreateReviewRequest request, List<IFormFile> files);

        Task<Response<string>> RemoveAsync(Guid id);
    }
}