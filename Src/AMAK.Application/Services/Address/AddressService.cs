using System.Security.Claims;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Interfaces;
using AMAK.Application.Services.Address.Dtos;
using AMAK.Domain.Models;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMAK.Application.Services.Address {
    public class AddressService : IAddressService {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IRepository<Domain.Models.Address> _addressRepository;

        private readonly IMapper _mapper;


        public AddressService(UserManager<ApplicationUser> userManager, IRepository<Domain.Models.Address> addressRepository, IMapper mapper) {
            _userManager = userManager;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Response<AddressResponse>> CreateAddressAsync(ClaimsPrincipal claims, AddressRequest request) {
            var currentUser = await _userManager.GetUserAsync(claims) ?? throw new BadRequestException("Account not found!");

            var existActiveAddress = _addressRepository.GetAll().Any(x => x.IsActive);

            var newAddress = _mapper.Map<Domain.Models.Address>(request);

            newAddress.Id = Guid.NewGuid();
            newAddress.UserId = currentUser.Id;
            newAddress.User = currentUser;
            newAddress.IsActive = !existActiveAddress;

            _addressRepository.Add(newAddress);

            await _addressRepository.SaveChangesAsync();

            return new Response<AddressResponse>(HttpStatusCode.OK, _mapper.Map<AddressResponse>(newAddress));
        }

        public async Task<Response<AddressResponse>> GetAddressDetailAsync(Guid id) {
            var existingAddress = await _addressRepository.GetById(id) ?? throw new NotFoundException("Address not found!");

            return new Response<AddressResponse>(HttpStatusCode.OK, _mapper.Map<AddressResponse>(existingAddress));
        }

        public async Task<PaginationResponse<List<AddressResponse>>> GetAddressesAsync(ClaimsPrincipal claims, BaseQuery query) {

            var currentUser = await _userManager.GetUserAsync(claims) ?? throw new BadRequestException("Account not found!");

            return await this.Pagination(currentUser, query);
        }

        public async Task<PaginationResponse<List<AddressResponse>>> GetAddressesUserAsync(string userId, BaseQuery query) {
            var currentUser = await _userManager.FindByIdAsync(userId) ?? throw new BadRequestException("Account not found!");

            return await this.Pagination(currentUser, query);
        }

        public async Task<Response<string>> RemoveAddressAsync(ClaimsPrincipal claims, Guid id) {
            var currentUser = await _userManager.GetUserAsync(claims) ?? throw new BadRequestException("Account not found!");

            var existingAddress = await _addressRepository.GetById(id) ?? throw new NotFoundException("Address not found!");

            if (!existingAddress.UserId!.Equals(currentUser.Id)) {
                throw new BadRequestException("You can only delete your address!");
            }

            if (existingAddress.IsActive) {
                throw new BadRequestException("You can't delete an active address!");
            }
            _addressRepository.Remove(existingAddress);

            await _addressRepository.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Address deleted successfully!");
        }

        public async Task<Response<AddressResponse>> UpdateAddressAsync(ClaimsPrincipal claims, Guid id, AddressRequest request) {
            var currentUser = await _userManager.GetUserAsync(claims) ?? throw new BadRequestException("Account not found!");

            var existingAddress = await _addressRepository.GetById(id) ?? throw new NotFoundException("Address not found!");

            if (!existingAddress.UserId!.Equals(currentUser.Id)) {
                throw new BadRequestException("You can only update your address!");
            }

            _mapper.Map(request, existingAddress);

            _addressRepository.Update(existingAddress);

            await _addressRepository.SaveChangesAsync();

            return new Response<AddressResponse>(HttpStatusCode.OK, _mapper.Map<AddressResponse>(existingAddress));

        }


        private async Task<PaginationResponse<List<AddressResponse>>> Pagination(ApplicationUser currentUser, BaseQuery query) {
            var totalItemsCount = await _addressRepository
                                       .GetAll()
                                       .CountAsync(x => x.UserId == currentUser.Id);

            var totalPages = (int)Math.Ceiling(totalItemsCount / (double)query.Limit);


            var addresses = await _addressRepository
                                        .GetAll()
                                        .Where(x => x.UserId == currentUser.Id)
                                        .Skip((query.Page - 1) * query.Limit)
                                        .Take(query.Limit)
                                        .OrderByDescending(x => x.CreateAt)
                                        .ToListAsync();


            var result = new PaginationResponse<List<AddressResponse>> {
                CurrentPage = query.Page,
                TotalPage = totalPages,
                Items = query.Limit,
                TotalItems = totalItemsCount,
                Result = _mapper.Map<List<AddressResponse>>(addresses)
            };

            return result;
        }
    }
}