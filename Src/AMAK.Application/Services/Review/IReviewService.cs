using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Review.Dtos;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Review {
    public interface IReviewService {
        Task<ListReviewResponse<List<ReviewResponse>>> GetAllAsync(Guid productId, ReviewQuery query);

        Task<ListReviewResponse<List<ReviewResponse>>> GetAsync(ClaimsPrincipal claims, ReviewQuery query);

        Task<Response<ReviewResponse>> GetOneAsync(Guid id);

        Task<Response<string>> CreateAsync(ClaimsPrincipal claims, CreateReviewRequest request, List<IFormFile> files);

        Task<Response<string>> RemoveAsync(Guid id);
    }
}