using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Billboard.Dtos;
using Microsoft.AspNetCore.Http;

namespace AMAK.Application.Services.Billboard {
    public interface IBillboardService {
        Task<Response<List<BillboardResponse>>> GetAllAsync();
        Task<Response<BillboardResponse>> CreateAsync(IFormFile file, CreateBillboardRequest request);
        Task<Response<string>> DeleteAsync(Guid id);

    }
}