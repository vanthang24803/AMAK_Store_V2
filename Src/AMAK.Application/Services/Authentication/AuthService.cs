using AMAK.Application.Common.Constants;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Authentication.Dtos;
using AMAK.Domain.Models;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AMAK.Application.Services.Mail;

namespace AMAK.Application.Services.Authentication {
    public class AuthService : IAuthService {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IMailService _mailService;

        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMailService mailService, IMapper mapper) {
            _userManager = userManager;
            _roleManager = roleManager;
            _mailService = mailService;
            _mapper = mapper;
        }


        public async Task<List<IdentityRole>> GetRoles() {
            return await _roleManager.Roles.ToListAsync();
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


            _mailService.SendEmailConfirmationAccount(request.Email, newUser.Id, token);


            var response = _mapper.Map<RegisterResponse>(newUser);


            return new Response<RegisterResponse>(HttpStatusCode.Created, response);
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


    }
}