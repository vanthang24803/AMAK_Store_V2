using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Me.Dtos;
using AMAK.Domain.Models;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Identity;
using AMAK.Application.Providers.Cloudinary;
using Microsoft.AspNetCore.Http;
using AMAK.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AMAK.Application.Services.Address.Dtos;
using AMAK.Domain.Enums;
using AMAK.Application.Providers.Cache;

namespace AMAK.Application.Services.Me {
    public class MeService : IMeService {

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudinaryService _CloudinaryService;
        private readonly IRepository<Domain.Models.Address> _addressRepository;
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly Dictionary<(double, double?), string> rank;
        private readonly ICacheService _cacheService;


        public MeService(IMapper mapper, UserManager<ApplicationUser> userManager, ICloudinaryService CloudinaryService, IRepository<Domain.Models.Address> addressRepository, IRepository<Domain.Models.Order> orderRepository, ICacheService cacheService) {
            _mapper = mapper;
            _userManager = userManager;
            _CloudinaryService = CloudinaryService;
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
            _cacheService = cacheService;
        }

        public async Task<Response<ProfileResponse>> GetProfileAsync(ClaimsPrincipal claims) {
            var existingUser = await _userManager.GetUserAsync(claims)
              ?? throw new NotFoundException("Account not found!");

            var cacheKey = $"Profile_{existingUser.Id}";

            var cachedData = await _cacheService.GetData<Response<ProfileResponse>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }


            var roles = await _userManager.GetRolesAsync(existingUser);

            var addresses = await _addressRepository.GetAll().Where(x => x.UserId == existingUser.Id).ToListAsync();

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.NumberPhone = existingUser.PhoneNumber;

            var ordersWithLatestStatus = await _orderRepository.GetAll()
                .Include(o => o.Status)
                .Where(o => o.UserId == existingUser.Id && !o.IsDeleted)
                .Select(o => new {
                    Order = o,
                    LatestStatus = o.Status
                        .OrderByDescending(st => st.TimeStamp)
                        .FirstOrDefault()
                })
                .ToListAsync();

            var successfulOrders = ordersWithLatestStatus
                .Where(o => o.LatestStatus != null && o.LatestStatus.Status == EOrderStatus.SUCCESS)
                .Select(o => o.Order)
                .ToList();

            var totalOrder = await _orderRepository.GetAll()
                .Where(x => x.UserId == existingUser.Id && !x.IsDeleted)
                .CountAsync();

            var orderProcessing = successfulOrders.Count;

            var totalPrice = await _orderRepository.GetAll()
                    .Where(x => x.UserId == existingUser.Id && !x.IsDeleted)
                    .SumAsync(x => x.TotalPrice);

            response.Roles = roles;
            response.TotalOrder = totalOrder;
            response.ProcessOrder = orderProcessing;
            response.TotalPrice = totalPrice;
            response.Rank = GetRank(totalPrice);
            response.Addresses = _mapper.Map<List<AddressResponse>>(addresses);

            var result = new Response<ProfileResponse>(HttpStatusCode.OK, response);

            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(1));

            return result;
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

            existingUser.PhoneNumber = request.NumberPhone;

            await _userManager.UpdateAsync(existingUser);

            await _cacheService.RemoveData($"Profile_{existingUser.Id}");

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }

        public async Task<Response<ProfileResponse>> UploadAvatarAsync(ClaimsPrincipal claims, IFormFile file) {
            var existingUser = await _userManager.GetUserAsync(claims)
             ?? throw new NotFoundException("Account not found!");

            var roles = await _userManager.GetRolesAsync(existingUser);

            var upload = await _CloudinaryService.UploadPhotoAsync(file);

            if (upload.Error != null) {
                throw new BadRequestException(message: upload.Error.Message);
            }

            existingUser.UpdateAt = DateTime.UtcNow;
            existingUser.Avatar = upload.SecureUrl.AbsoluteUri;

            await _userManager.UpdateAsync(existingUser);

            await _cacheService.RemoveData($"Profile_{existingUser.Id}");

            var response = _mapper.Map<ProfileResponse>(existingUser);

            response.Roles = roles;

            return new Response<ProfileResponse>(HttpStatusCode.OK, response);
        }

        private string GetRank(double totalPrice) {
            var rankThreshold = rank.Keys.LastOrDefault(k => k.Item1 <= totalPrice && (k.Item2 == null || k.Item2 >= totalPrice));

            if (!rankThreshold.Equals(default)) {
                return rank[rankThreshold];
            } else {
                return string.Empty;
            }
        }
    }
}