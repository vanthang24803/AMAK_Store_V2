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
using AMAK.Application.Providers.Mail.Dtos;
using Microsoft.Extensions.Logging;
using AMAK.Application.Providers.RabbitMq;
using AMAK.Application.Common.Constants;
using AMAK.Application.Providers.RabbitMq.Common;
using AMAK.Application.Constants;

namespace AMAK.Application.Services.Me {
    public class MeService : IMeService {

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IRepository<Domain.Models.Address> _addressRepository;
        private readonly IRepository<Domain.Models.Order> _orderRepository;
        private readonly Dictionary<(double, double?), string> _rank;
        private readonly ICacheService _cacheService;
        private readonly ILogger _logger;
        private readonly IRabbitProducer _rabbitProducer;

        public MeService(IMapper mapper, UserManager<ApplicationUser> userManager, ICloudinaryService cloudinaryService, IRepository<Domain.Models.Address> addressRepository, IRepository<Domain.Models.Order> orderRepository, ICacheService cacheService, ILogger<MeService> logger, IRabbitProducer rabbitProducer) {
            _mapper = mapper;
            _userManager = userManager;
            _cloudinaryService = cloudinaryService;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;

            _rank = new Dictionary<(double, double?), string>()
            {
                { (0 , 100000), "Bronze" },
                { (100000, 500000),"Silver" },
                { (500000 , 1500000),"Gold" },
                { (1500000 , 3500000),"Platinum" },
                { (3500000, null),"Diamond" }
            };
            _cacheService = cacheService;
            _logger = logger;
            _rabbitProducer = rabbitProducer;
        }

        public async Task<Response<string>> GenerateOTPEmail(ClaimsPrincipal claims, SendOTPEmailRequest request) {
            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            var cacheKey = $"OTP_{existingUser.Id}";
            var cachedData = await _cacheService.GetData<Response<string>>(cacheKey);

            if (cachedData != null) {
                return cachedData;
            }

            var otp = await _userManager.GetAuthenticationTokenAsync(existingUser, Provider.Account, Token.OTPToken);
            if (string.IsNullOrEmpty(otp)) {
                otp = Util.GenerateOTP();
                await _userManager.SetAuthenticationTokenAsync(existingUser, Provider.Account, Token.OTPToken, otp);
            }

            _rabbitProducer.SendMessage(RabbitQueue.MailQueue, new MailWithTokenEvent {
                UserId = existingUser.Id,
                Email = existingUser.Email ?? "",
                FullName = $"{existingUser.FirstName} {existingUser.LastName}",
                Token = otp,
                Type = EEmailType.OTP_EMAIL
            });

            var result = new Response<string>(HttpStatusCode.OK, "Send OTP Mail Successfully!");
            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(5));

            return result;
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

        public async Task<Response<string>> UpdateEmailAsync(ClaimsPrincipal claimsPrincipal, UpdateEmailRequest request) {
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null) {
                throw new BadRequestException("Email already exists!");
            }

            var existingUser = await _userManager.GetUserAsync(claimsPrincipal)
                ?? throw new NotFoundException("Account not found!");

            var otp = await _userManager.GetAuthenticationTokenAsync(existingUser, Provider.Account, Token.OTPToken);
            if (otp == null || request.Code != otp) {
                throw new BadRequestException("Invalid OTP!");
            }

            await _userManager.RemoveAuthenticationTokenAsync(existingUser, Provider.Account, Token.OTPToken);

            existingUser.UserName = request.Email;
            existingUser.Email = request.Email;

            var updateResult = await _userManager.UpdateAsync(existingUser);
            if (!updateResult.Succeeded) {
                throw new BadRequestException("Failed to update email!");
            }

            await _cacheService.RemoveData($"Profile_{existingUser.Id}");

            await _cacheService.RemoveData($"OTP_{existingUser.Id}");

            return new Response<string>(HttpStatusCode.OK, "Updated email successfully!");
        }


        public async Task<Response<string>> UpdateFullName(ClaimsPrincipal claimsPrincipal, UpdateFullNameRequest request) {
            var existingUser = await _userManager.GetUserAsync(claimsPrincipal)
            ?? throw new NotFoundException("Account not found!");

            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;

            await _userManager.UpdateAsync(existingUser);

            await _cacheService.RemoveData($"Profile_{existingUser.Id}");

            return new Response<string>(HttpStatusCode.OK, "Updated Name Successfully!");
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

            var upload = await _cloudinaryService.UploadPhotoAsync(file);

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
            var rankThreshold = _rank.Keys.LastOrDefault(k => k.Item1 <= totalPrice && (k.Item2 == null || k.Item2 >= totalPrice));

            if (!rankThreshold.Equals(default)) {
                return _rank[rankThreshold];
            } else {
                return string.Empty;
            }
        }
    }
}