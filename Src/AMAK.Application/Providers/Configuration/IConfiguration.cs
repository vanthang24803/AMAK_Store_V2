using AMAK.Application.Common.Helpers;
using AMAK.Application.Providers.Configuration.Dtos;

namespace AMAK.Application.Providers.Configuration {
    public interface IConfigurationProvider {
        Task<Response<string>> UpdateEmailSettingAsync(MailSettings mailSettings);
        Task<Response<MailSettings>> GetEmailSettingAsync();
        Task<Response<string>> UpdateGoogleSettingAsync(GoogleSettings googleSettings);
        Task<Response<GoogleSettings>> GetGoogleSettingAsync();
        Task<Response<string>> UpdateCloudinarySettingAsync(CloudinarySettings cloudinarySettings);
        Task<Response<CloudinarySettings>> GetCloudinarySettingAsync();

        Task<Response<string>> UpdateMomoSettingAsync(MomoSettings momo);
        Task<Response<MomoSettings>> GetMomoSettingAsync();

        Task<Response<string>> UpdateGeminiConfig(GeminiSettings geminiSettings);
        Task<Response<GeminiSettings>> GetGeminiConfigAsync();
    }
}