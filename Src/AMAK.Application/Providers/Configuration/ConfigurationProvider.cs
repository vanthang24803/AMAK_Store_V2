using System.Text.Json;
using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using AMAK.Application.Providers.Configuration.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AMAK.Application.Providers.Configuration {
    public class ConfigurationProvider : IConfigurationProvider {
        private readonly IRepository<Domain.Models.Configuration> _configurationRepository;

        public ConfigurationProvider(IRepository<Domain.Models.Configuration> configurationRepository) {
            _configurationRepository = configurationRepository;
        }

        public Task<Response<CloudinarySettings>> GetCloudinarySettingAsync() {
            return GetSettingAsync<CloudinarySettings>(Constants.Configuration.CLOUDINARY);
        }

        public Task<Response<MailSettings>> GetEmailSettingAsync() {
            return GetSettingAsync<MailSettings>(Constants.Configuration.EMAIL);
        }

        public Task<Response<GoogleSettings>> GetGoogleSettingAsync() {
            return GetSettingAsync<GoogleSettings>(Constants.Configuration.GOOGLE);
        }

        public Task<Response<MomoSettings>> GetMomoSettingAsync() {
            return GetSettingAsync<MomoSettings>(Constants.Configuration.MOMO);
        }

        public async Task<Response<string>> UpdateCloudinarySettingAsync(CloudinarySettings cloudinarySettings) {
            return await UpdateSettingAsync(cloudinarySettings, Constants.Configuration.CLOUDINARY, "Cloudinary settings updated successfully.");
        }

        public async Task<Response<string>> UpdateEmailSettingAsync(MailSettings mailSettings) {
            return await UpdateSettingAsync(mailSettings, Constants.Configuration.EMAIL, "Mail settings updated successfully.");
        }

        public async Task<Response<string>> UpdateGoogleSettingAsync(GoogleSettings googleSettings) {
            return await UpdateSettingAsync(googleSettings, Constants.Configuration.GOOGLE, "Google settings updated successfully.");
        }

        public async Task<Response<string>> UpdateMomoSettingAsync(MomoSettings momo) {
            return await UpdateSettingAsync(momo, Constants.Configuration.MOMO, "Momo settings updated successfully.");
        }

        private async Task<Response<string>> UpdateSettingAsync<TSettings>(TSettings settings, string key, string successMessage) {
            var existingSettings = await _configurationRepository.GetAll().FirstOrDefaultAsync(x => x.Key == key);

            // TODO: Serialize the settings into JsonElement
            var serializedSettings = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(settings));

            if (existingSettings == null) {
                //TODO: Create new settings if not found
                var newSettings = new Domain.Models.Configuration {
                    Id = Guid.NewGuid(),
                    Key = key,
                    Value = serializedSettings
                };

                _configurationRepository.Add(newSettings);
            } else {
                //TODO: Update existing settings
                existingSettings.Value = serializedSettings;
                _configurationRepository.Update(existingSettings);
            }

            await _configurationRepository.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, successMessage);
        }



        private async Task<Response<TSettings>> GetSettingAsync<TSettings>(string key) where TSettings : class {
            var settingsData = await _configurationRepository.GetAll().FirstOrDefaultAsync(x => x.Key == key)
                ?? throw new NotFoundException($"{key} setting not found!");

            if (!settingsData.Value.HasValue) {
                throw new BadRequestException($"{key} settings value is null.");
            }

            var settings = JsonSerializer.Deserialize<TSettings>(
                settingsData.Value.Value.GetRawText()
            ) ?? throw new BadRequestException($"Could not deserialize {key} settings.");

            return new Response<TSettings>(HttpStatusCode.OK, settings);
        }
    }
}