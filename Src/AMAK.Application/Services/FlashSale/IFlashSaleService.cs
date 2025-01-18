using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.FlashSale.Dtos;

namespace AMAK.Application.Services.FlashSale {
    public interface IFlashSaleService {
        Task<Response<string>> CreateAsync(CreateFlashSaleRequest request);
        Task<Response<FlashSaleResponse>> FindOne(Guid id);
        Task<Response<List<ListFlashSaleResponse>>> FindAll();
        Task<Response<FlashSaleResponse>> GetFlashSaleAsync();
        Task JobFlashSale();
    }
}