using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AMAK.Application.Providers.Configuration;
using AMAK.Application.Providers.Configuration.Dtos;
using AMAK.Application.Providers.Momo.Dtos;
using AMAK.Application.Services.Order.Dtos;

namespace AMAK.Application.Providers.Momo {
    public class MomoService : IMomoService {

        private readonly MomoSettings _momoSettings;

        private static readonly HttpClient HttpClient = new();

        public MomoService(IConfigurationProvider configurationProvider) {
            _momoSettings = InitializeConfig(configurationProvider).GetAwaiter().GetResult();
        }

        private static async Task<MomoSettings> InitializeConfig(IConfigurationProvider configurationProvider) {
            var cloudinarySettingsResponse = await configurationProvider.GetMomoSettingAsync();
            var settings = cloudinarySettingsResponse.Result;

            return settings;
        }

        public async Task<string> CreateMomoPaymentAsync(CreateOrderRequest dataRequest) {

            var request = new MomoRequest {
                orderInfo = "pay with MoMo",
                partnerCode = _momoSettings.PartnerCode,
                redirectUrl = _momoSettings.ReturnUrl,
                ipnUrl = _momoSettings.IpnUrl,
                amount = (long)dataRequest.TotalPrice,
                orderId = dataRequest.Id.ToString(),
                requestId = Guid.NewGuid().ToString(),
                extraData = "",
                orderExpireTime = 5,
                partnerName = "MoMo Payment",
                storeId = "AMAK Store",
                requestType = "captureWallet",
                orderGroupId = "",
                autoCapture = true,
                lang = "vi"
            };

            var rawSignature = "accessKey=" + _momoSettings.AccessKey +
                                "&amount=" + request.amount +
                                "&extraData=" + request.extraData +
                                "&ipnUrl=" + request.ipnUrl +
                                "&orderId=" + request.orderId +
                                "&orderInfo=" + request.orderInfo +
                                "&partnerCode=" + request.partnerCode +
                                "&redirectUrl=" + request.redirectUrl +
                                "&requestId=" + request.requestId +
                                "&requestType=" + request.requestType;

            request.signature = GetSignature(rawSignature, _momoSettings.SecretKey);

            StringContent httpContent = new(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var quickPayResponse = await HttpClient.PostAsync(_momoSettings.PaymentUrl, httpContent);
            var contents = quickPayResponse.Content.ReadAsStringAsync().Result;
            return contents;
        }

        private static string GetSignature(string text, string key) {
            ASCIIEncoding encoding = new();

            var textBytes = encoding.GetBytes(text);
            var keyBytes = encoding.GetBytes(key);

            byte[] hashBytes;

            using (HMACSHA256 hash = new(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }


    }
}