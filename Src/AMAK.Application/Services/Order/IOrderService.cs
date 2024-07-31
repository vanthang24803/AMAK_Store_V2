using System.Security.Claims;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Order.Dtos;

namespace AMAK.Application.Services.Order {
    public interface IOrderService {
        Task<Response<string>> CreateAsync(ClaimsPrincipal user, CreateOrderRequest request);
        Task<PaginationResponse<List<OrderResponse>>> GetByUser(ClaimsPrincipal user, BaseQuery baseQuery);
        Task<Response<OrderResponse>> GetAsync(Guid id);

        Task<Response<string>> DeleteAsync(ClaimsPrincipal user, Guid id);
    }
}