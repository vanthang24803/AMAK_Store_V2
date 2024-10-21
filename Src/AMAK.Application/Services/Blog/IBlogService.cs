using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Blog.Dto;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Blog {
    public interface IBlogService {
        Task<PaginationResponse<List<BlogResponse>>> GetAllBlogAsync(BaseQuery query);
        Task<PaginationResponse<List<BlogResponse>>> GetAllBlogForAccountAsync(ClaimsPrincipal claims, BaseQuery query);
        Task<Response<BlogResponse>> FindOneByIdAsync(Guid id);
        Task<Response<string>> CreateAsync(ClaimsPrincipal claims, BlogRequest request, IFormFile file);
        Task<Response<string>> UpdateAsync(ClaimsPrincipal claims, Guid blogId, BlogRequest request, IFormFile? file);
        Task<Response<string>> DeleteAsync(ClaimsPrincipal claims, Guid blogId);

    }
}