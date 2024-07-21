using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Domain.Models;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Identity;
using AMAK.Application.Providers.Upload;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Me {
    public class MeService : IMeService {

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUploadService _uploadService;

        public MeService(IMapper mapper, UserManager<ApplicationUser> userManager, IUploadService uploadService) {
            _mapper = mapper;
            _userManager = userManager;
            _uploadService = uploadService;
        }

        public async Task<Response<ProfileResponse>> GetProfileAsync(ClaimsPrincipal claims) {

            var existingUser = await _userManager.GetUserAsync(claims)
              ?? throw new NotFoundException("Account not found!");

            var roles = await _userManager.GetRolesAsync(existingUser);

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }

        public async Task<Response<string>> UpdatePasswordAsync(ClaimsPrincipal claims, UpdatePasswordRequest request) {

            var existingUser = await _userManager.GetUserAsync(claims)
           ?? throw new NotFoundException("Account not found!");

            var isMatchPassword = await _userManager.CheckPasswordAsync(existingUser, request.OldPassword);

            if (!isMatchPassword) {
                throw new BadRequestException("Password is not match!");
            }

            var updatePassword = await _userManager.ChangePasswordAsync(existingUser, request.OldPassword, request.NewPassword);

            if (!updatePassword.Succeeded) {
                throw new BadRequestException("Password updating error!");
            }

            return new Response<string>(HttpStatusCode.OK, "Password updated successfully!");
        }

        public async Task<Response<ProfileResponse>> UpdateProfileAsync(ClaimsPrincipal claims, UpdateProfileRequest request) {

            var existingUser = await _userManager.GetUserAsync(claims)
             ?? throw new NotFoundException("Account not found!");

            var roles = await _userManager.GetRolesAsync(existingUser);

            _mapper.Map(request, existingUser);

            await _userManager.UpdateAsync(existingUser);

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }

        public async Task<Response<ProfileResponse>> UploadAvatarAsync(ClaimsPrincipal claims, IFormFile file) {
            var existingUser = await _userManager.GetUserAsync(claims)
             ?? throw new NotFoundException("Account not found!");

            var roles = await _userManager.GetRolesAsync(existingUser);

            var upload = await _uploadService.UploadPhotoAsync(file);

            if (upload.Error != null) {
                throw new BadRequestException(message: upload.Error.Message);
            }

            existingUser.UpdateAt = DateTime.UtcNow;
            existingUser.Avatar = upload.SecureUrl.AbsoluteUri;

            await _userManager.UpdateAsync(existingUser);


            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }
    }
}