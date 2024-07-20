using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Application.Services.Mail;
using AMAK.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace AMAK.Application.Services.Authentication {
    public class AuthService : IAuthService {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMailService _mailService;

        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMailService mailService, IMapper mapper, ITokenService tokenService) {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request) {

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null) {
                throw new BadRequestException("Email existed!");
            }

            var newUser = _mapper.Map<ApplicationUser>(request);

            newUser.UserName = request.Email;
            newUser.SecurityStamp = Guid.NewGuid().ToString();


            var createUserResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!createUserResult.Succeeded) {
                throw new BadRequestException("Wrong Data!");
            }

            if (!await _roleManager.RoleExistsAsync(StaticRole.CUSTOMER)) {
                throw new BadRequestException("Customer Role Not found!");
            }

            await _userManager.AddToRoleAsync(newUser, StaticRole.CUSTOMER);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);


            _mailService.SendEmailConfirmationAccount(newUser.UserName, newUser.Id, token);


            var response = _mapper.Map<RegisterResponse>(newUser);


            return new Response<RegisterResponse>(HttpStatusCode.Created, response);
        }

        public async Task<Response<TokenResponse>> VerifyAccountAsync(string userId, string token) {
            var existingUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("Account not found!");
            var roles = await _userManager.GetRolesAsync(existingUser);

            var confirmAccount = await _userManager.ConfirmEmailAsync(existingUser, token);

            if (!confirmAccount.Succeeded) {
                throw new UnauthorizedException();
            }

            var response = _tokenService.GenerateToken(existingUser, roles);

            return new Response<TokenResponse>(HttpStatusCode.OK, response);
        }


        public async Task<Response<TokenResponse>> LoginAsync(LoginRequest request) {
            var existingUser = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException("Account not found!");

            if (!existingUser.EmailConfirmed) {
                throw new BadRequestException("Account not verify email!");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(existingUser, request.Password);

            if (!isPasswordCorrect) {
                throw new UnauthorizedException();
            }

            var userRoles = await _userManager.GetRolesAsync(existingUser);

            var token = _tokenService.GenerateToken(existingUser, userRoles);

            await _userManager.RemoveAuthenticationTokenAsync(existingUser, ProviderStatic.Account, TokenStatic.RefreshToken);

            await _userManager.SetAuthenticationTokenAsync(existingUser, ProviderStatic.Account, TokenStatic.RefreshToken, token.RefreshToken);

            return new Response<TokenResponse>(HttpStatusCode.OK, token);
        }


        public async Task<Response<TokenResponse>> RefreshTokenAsync(TokenRequest request) {

            var decode = _tokenService.DecodeRefreshToken(request.Token);

            var existingUser = await _userManager.FindByIdAsync(decode.Sub)
                ?? throw new NotFoundException("Account not found!");

            var existingRoles = await _userManager.GetRolesAsync(existingUser);

            var existingToken = await _userManager.GetAuthenticationTokenAsync(existingUser, ProviderStatic.Account, TokenStatic.RefreshToken)
                ?? throw new NotFoundException("Token not found!");

            if (!existingToken.Equals(request.Token)) {
                throw new BadRequestException("Token doesn't match!");
            }

            var accessToken = _tokenService.GenerateAccessToken(existingUser, existingRoles);

            var exp = DateTimeOffset.FromUnixTimeSeconds((long)decode.Expiration!).UtcDateTime;

            TokenResponse tokenResponse;

            if (exp >= DateTime.Now) {
                tokenResponse = new TokenResponse() {
                    AccessToken = accessToken,
                    RefreshToken = existingToken
                };
            } else {

                await _userManager.RemoveAuthenticationTokenAsync(existingUser, ProviderStatic.Account, TokenStatic.RefreshToken);

                var newRefreshToken = _tokenService.GenerateRefreshToken(existingUser, existingRoles);

                await _userManager.SetAuthenticationTokenAsync(existingUser, ProviderStatic.Account, TokenStatic.RefreshToken, newRefreshToken);

                tokenResponse = new TokenResponse() {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken
                };
            }

            return new Response<TokenResponse>(HttpStatusCode.OK, tokenResponse);
        }


        public async Task<Response<string>> LogoutAsync(ClaimsPrincipal claims) {

            var existingUser = await _userManager.GetUserAsync(claims)
                ?? throw new NotFoundException("Account not found!");

            await _userManager.RemoveAuthenticationTokenAsync(existingUser, ProviderStatic.Account, TokenStatic.RefreshToken);


            return new Response<string>(HttpStatusCode.OK, "Logout successfully!");
        }


        public async Task<List<IdentityRole>> GetRoles() {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<string> CreateSeedRole() {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticRole.MANAGER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticRole.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticRole.CUSTOMER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists) throw new BadRequestException("Roles Seeding is Already Done");


            await _roleManager.CreateAsync(new IdentityRole(StaticRole.CUSTOMER));
            await _roleManager.CreateAsync(new IdentityRole(StaticRole.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticRole.MANAGER));

            return "OK";
        }

        public async Task<Response<string>> ResetPasswordAsync(string userId, string token, ResetPasswordRequest request) {

            var existingUser = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("Account not found!");


            var updatePassword = await _userManager.ResetPasswordAsync(existingUser, token, request.NewPassword);

            if (!updatePassword.Succeeded) {
                throw new BadRequestException("Token doesn't match!");
            }

            return new Response<string>(HttpStatusCode.OK, "Reset password successfully!");
        }

        public async Task<Response<string>> ForgotPasswordAsync(ForgotPasswordRequest request) {

            var existingUser = await _userManager.FindByEmailAsync(request.Email) ?? throw new NotFoundException("Account not found!");

            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);


            _mailService.SendMailResetPassword(request.Email, existingUser.Id, token);


            return new Response<string>(HttpStatusCode.OK, "Send mail reset password successfully!");
        }
    }
}