using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Review.Dtos;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Review {
    public interface IReviewService {
        Task<List<Domain.Models.Review>> GetAllAsync(Guid productId);

        Task<Response<Domain.Models.Review>> GetAsync(ClaimsPrincipal claims, Guid productId, Guid id);

        Task<Response<string>> CreateAsync(ClaimsPrincipal claims, CreateReviewRequest request, List<IFormFile> files);
    }
}