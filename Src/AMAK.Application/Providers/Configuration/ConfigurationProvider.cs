using System.Text.Json;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Configuration.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using AMAK.Application.Providers.Cache;

namespace AMAK.Application.Providers.Configuration {
    public class ConfigurationProvider : IConfigurationProvider {
        private readonly IRepository<Domain.Models.Configuration> _configurationRepository;

        private readonly ICacheService _cacheService;

        public ConfigurationProvider(IRepository<Domain.Models.Configuration> configurationRepository, ICacheService cacheService) {
            _configurationRepository = configurationRepository;
            _cacheService = cacheService;
        }

        public async Task<Response<CloudinarySettings>> GetCloudinarySettingAsync() =>
            await GetSettingsAsync<CloudinarySettings>("Config_Cloudinary", Constants.Configuration.CLOUDINARY);

        public async Task<Response<MailSettings>> GetEmailSettingAsync() =>
            await GetSettingsAsync<MailSettings>("Config_Mail", Constants.Configuration.EMAIL);

        public async Task<Response<GoogleSettings>> GetGoogleSettingAsync() =>
            await GetSettingsAsync<GoogleSettings>("Config_Google", Constants.Configuration.GOOGLE);

        public async Task<Response<MomoSettings>> GetMomoSettingAsync() =>
            await GetSettingsAsync<MomoSettings>("Config_Momo", Constants.Configuration.MOMO);

        public async Task<Response<GeminiSettings>> GetGeminiConfigAsync() =>
            await GetSettingsAsync<GeminiSettings>("Config_Gemini", Constants.LLM.GEMINI);

        public async Task<Response<Config>> GetAllConfig() {
            var config = new Config {
                Cloudinary = (await GetAndDecodeSettingsAsync<CloudinarySettings>(Constants.Configuration.CLOUDINARY)).Result,
                Mail = (await GetAndDecodeSettingsAsync<MailSettings>(Constants.Configuration.EMAIL)).Result,
                Google = (await GetAndDecodeSettingsAsync<GoogleSettings>(Constants.Configuration.GOOGLE)).Result,
                Momo = (await GetAndDecodeSettingsAsync<MomoSettings>(Constants.Configuration.MOMO)).Result,
                GeminiSettings = (await GetAndDecodeSettingsAsync<GeminiSettings>(Constants.LLM.GEMINI)).Result
            };

            return new Response<Config>(HttpStatusCode.OK, config);
        }

        public async Task<Response<string>> UpdateAllConfig(Config settings) {
            await UpdateSettingsAsync(settings.Cloudinary, "Config_Cloudinary", Constants.Configuration.CLOUDINARY);
            await UpdateSettingsAsync(settings.Mail, "Config_Mail", Constants.Configuration.EMAIL);
            await UpdateSettingsAsync(settings.Google, "Config_Google", Constants.Configuration.GOOGLE);
            await UpdateSettingsAsync(settings.Momo, "Config_Momo", Constants.Configuration.MOMO);
            await UpdateSettingsAsync(settings.GeminiSettings, "Config_Gemini", Constants.LLM.GEMINI);

            return new Response<string>(HttpStatusCode.OK, "All configurations updated successfully.");
        }

        public async Task<Response<string>> UpdateCloudinarySettingAsync(CloudinarySettings settings) =>
            await UpdateSettingsAsync(settings, "Config_Cloudinary", Constants.Configuration.CLOUDINARY);

        public async Task<Response<string>> UpdateEmailSettingAsync(MailSettings settings) =>
            await UpdateSettingsAsync(settings, "Config_Mail", Constants.Configuration.EMAIL);

        public async Task<Response<string>> UpdateGoogleSettingAsync(GoogleSettings settings) =>
            await UpdateSettingsAsync(settings, "Config_Google", Constants.Configuration.GOOGLE);

        public async Task<Response<string>> UpdateMomoSettingAsync(MomoSettings settings) =>
            await UpdateSettingsAsync(settings, "Config_Momo", Constants.Configuration.MOMO);

        public async Task<Response<string>> UpdateGeminiConfig(GeminiSettings settings) =>
             await UpdateSettingsAsync(settings, "Config_Gemini", Constants.LLM.GEMINI);


        public async Task<Response<TSettings>> GetSettingsAsync<TSettings>(string cacheKey, string configKey) where TSettings : class {
            var cachedData = await _cacheService.GetData<Response<TSettings>>(cacheKey);
            if (cachedData != null) {
                return cachedData;
            }

            var result = await GetAndDecodeSettingsAsync<TSettings>(configKey);
            await _cacheService.SetData(cacheKey, result, DateTimeOffset.UtcNow.AddMinutes(30));

            return result;
        }

        public async Task<Response<string>> UpdateSettingsAsync<TSettings>(TSettings settings, string cacheKey, string configKey) {
            var result = await UpdateAndEncodeSettingsAsync(settings, configKey);
            await _cacheService.RemoveData(cacheKey);
            return result;
        }

        private async Task<Response<TSettings>> GetAndDecodeSettingsAsync<TSettings>(string key) where TSettings : class {
            var settings = await GetSettingAsync<TSettings>(key);
            DecodeBase64Properties(settings);
            return new Response<TSettings>(HttpStatusCode.OK, settings);
        }

        private async Task<Response<string>> UpdateAndEncodeSettingsAsync<TSettings>(TSettings settings, string key) {
            EncodeBase64Properties(settings);
            return await UpdateSettingAsync(settings, key, "Settings updated successfully.");
        }


        private async Task<TSettings> GetSettingAsync<TSettings>(string key) where TSettings : class {
            var setting = await _configurationRepository.GetAll().FirstOrDefaultAsync(x => x.Key == key)
                ?? throw new NotFoundException($"{key} setting not found!");

            if (!setting.Value.HasValue) {
                throw new BadRequestException($"{key} settings value is null.");
            }

            return JsonSerializer.Deserialize<TSettings>(setting.Value.Value.GetRawText())
                ?? throw new BadRequestException($"Could not deserialize {key} settings.");
        }


        private async Task<Response<string>> UpdateSettingAsync<TSettings>(TSettings settings, string key, string successMessage) {
            var existingSettings = await _configurationRepository.GetAll().FirstOrDefaultAsync(x => x.Key == key);
            var serializedSettings = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(settings));

            if (existingSettings == null) {
                var newSettings = new Domain.Models.Configuration {
                    Id = Guid.NewGuid(),
                    Key = key,
                    Value = serializedSettings
                };
                _configurationRepository.Add(newSettings);
            } else {
                existingSettings.Value = serializedSettings;
                _configurationRepository.Update(existingSettings);
            }

            await _configurationRepository.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, successMessage);
        }

        private static void EncodeBase64Properties<TSettings>(TSettings settings) {
            if (settings == null) return;

            foreach (var prop in settings.GetType().GetProperties().Where(p => p.PropertyType == typeof(string))) {
                if (prop.GetValue(settings) is string value) {
                    prop.SetValue(settings, EncodeBase64(value));
                }
            }
        }

        private static void DecodeBase64Properties<TSettings>(TSettings settings) {
            if (settings == null) return;

            foreach (var prop in settings.GetType().GetProperties().Where(p => p.PropertyType == typeof(string))) {
                if (prop.GetValue(settings) is string value) {
                    prop.SetValue(settings, DecodeBase64(value));
                }
            }
        }

        public static string EncodeBase64(string value) => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        public static string DecodeBase64(string value) => Encoding.UTF8.GetString(Convert.FromBase64String(value));


    }
}
