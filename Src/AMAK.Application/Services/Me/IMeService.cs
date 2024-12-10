using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Me.Dtos;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Me {
    public interface IMeService {
        Task<Response<ProfileResponse>> GetProfileAsync(ClaimsPrincipal claims);
        Task<Response<ProfileResponse>> UpdateProfileAsync(ClaimsPrincipal claims, UpdateProfileRequest request);
        Task<Response<ProfileResponse>> UploadAvatarAsync(ClaimsPrincipal claims, IFormFile file);
        Task<Response<string>> UpdatePasswordAsync(ClaimsPrincipal claims, UpdatePasswordRequest request);
        Task<Response<string>> UpdateFullName(ClaimsPrincipal claimsPrincipal, UpdateFullNameRequest request);
        Task<Response<string>> GenerateOTPEmail(ClaimsPrincipal claims, SendOTPEmailRequest request);
        Task<Response<string>> UpdateEmailAsync(ClaimsPrincipal claimsPrincipal, UpdateEmailRequest request);
    }
}