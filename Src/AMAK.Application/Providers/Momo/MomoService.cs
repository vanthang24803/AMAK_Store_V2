using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AMAK.Application.Services.Order.Dtos;
using Microsoft.Extensions.Options;

namespace AMAK.Application.Providers.Momo {
    public class MomoService : IMomoService {

        private static readonly HttpClient _httpClient = new();
        private readonly MomoSettings _momoSettings;


        public MomoService(IOptions<MomoSettings> options) {
            _momoSettings = options.Value;
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
            var quickPayResponse = await _httpClient.PostAsync(_momoSettings.PaymentUrl, httpContent);
            var contents = quickPayResponse.Content.ReadAsStringAsync().Result;
            return contents;
        }

        private static string GetSignature(string text, string key) {
            ASCIIEncoding encoding = new();

            byte[] textBytes = encoding.GetBytes(text);
            byte[] keyBytes = encoding.GetBytes(key);

            byte[] hashBytes;

            using (HMACSHA256 hash = new(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }


    }
}