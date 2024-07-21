using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Address.Dtos;

namespace AMAK.Application.Services.Address {
    public interface IAddressService {
        Task<PaginationResponse<List<AddressResponse>>> GetAddressesUserAsync(string userId , BaseQuery baseQuery);

        Task<PaginationResponse<List<AddressResponse>>> GetAddressesAsync(ClaimsPrincipal claims, BaseQuery baseQuery);

        Task<Response<AddressResponse>> GetAddressDetailAsync(Guid id);
        Task<Response<AddressResponse>> CreateAddressAsync(ClaimsPrincipal claims, AddressRequest request);
        Task<Response<AddressResponse>> UpdateAddressAsync(ClaimsPrincipal claims, Guid id, AddressRequest request);

        Task<Response<string>> RemoveAddressAsync(ClaimsPrincipal claims, Guid id);
    }
}