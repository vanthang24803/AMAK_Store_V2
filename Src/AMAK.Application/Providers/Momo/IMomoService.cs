using AMAK.Application.Services.Order.Dtos;

namespace AMAK.Application.Providers.Momo {
    public interface IMomoService {
        Task<string> CreateMomoPaymentAsync(CreateOrderRequest request);

    }
}