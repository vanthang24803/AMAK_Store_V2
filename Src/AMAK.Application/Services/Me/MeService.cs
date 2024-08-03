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
using AMAK.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AMAK.Application.Services.Address.Dtos;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Me {
    public class MeService : IMeService {

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUploadService _uploadService;
        private readonly IRepository<Domain.Models.Address> _addressRepository;
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly Dictionary<(double, double?), string> rank;


        public MeService(IMapper mapper, UserManager<ApplicationUser> userManager, IUploadService uploadService, IRepository<Domain.Models.Address> addressRepository, IRepository<Domain.Models.Order> orderRepository) {
            _mapper = mapper;
            _userManager = userManager;
            _uploadService = uploadService;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;

            rank = new Dictionary<(double, double?), string>()
            {
                { (0 , 100000), "Bronze" },
                { (100000, 500000),"Silver" },
                { (500000 , 1500000),"Gold" },
                { (1500000 , 3500000),"Platinum" },
                { (3500000, null),"Diamond" }
            };
        }

        public async Task<Response<ProfileResponse>> GetProfileAsync(ClaimsPrincipal claims) {

            var existingUser = await _userManager.GetUserAsync(claims)
              ?? throw new NotFoundException("Account not found!");

            var roles = await _userManager.GetRolesAsync(existingUser);

            var addresses = await _addressRepository.GetAll().Where(x => x.UserId == existingUser.Id).ToListAsync();

            var response = _mapper.Map<ProfileResponse>(existingUser);

            var totalOrder = await _orderRepository.GetAll()
                .Where(x => x.UserId == existingUser.Id && !x.IsDeleted)
                .CountAsync();

            var orderProcessing = await _orderRepository.GetAll()
                .Where(x => x.UserId == existingUser.Id && !x.IsDeleted && !x.Status.Equals(EOrderStatus.SUCCESS))
                .CountAsync();

            var totalPrice = await _orderRepository.GetAll()
                    .Where(x => x.UserId == existingUser.Id && !x.IsDeleted && x.Status.Equals(EOrderStatus.SUCCESS))
                    .SumAsync(x => x.TotalPrice);

            response.Roles = roles;
            response.TotalOrder = totalOrder;
            response.ProcessOrder = orderProcessing;
            response.TotalPrice = totalPrice;
            response.Rank = GetRank(totalPrice);
            response.Addresses = _mapper.Map<List<AddressResponse>>(addresses);

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

        private string GetRank(double totalPrice) {
            var rankThreshold = rank.Keys.LastOrDefault(k => k.Item1 <= totalPrice && (k.Item2 == null || k.Item2 >= totalPrice));

            if (!rankThreshold.Equals(default((double, double?)))) {
                return rank[rankThreshold];
            } else {
                return string.Empty;
            }
        }
    }
}