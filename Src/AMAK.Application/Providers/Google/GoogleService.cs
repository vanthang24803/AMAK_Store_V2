using AMAK.Application.Providers.Configuration;
using AMAK.Application.Providers.Configuration.Dtos;
using Google.Apis.Auth.OAuth2;

namespace AMAK.Application.Providers.Google {

    public class GoogleService : IGoogleService {
        private readonly GoogleSettings _googleConfig;
        private readonly string[] scopes = [
            Constants.Google.GoogleMailReadonly,
            Constants.Google.GoogleMailUserInfo
        ];

        public GoogleService(IConfigurationProvider configurationProvider) {
            _googleConfig = InitializeConfig(configurationProvider).GetAwaiter().GetResult();
        }

        private static async Task<GoogleSettings> InitializeConfig(IConfigurationProvider configurationProvider) {
            var cloudinarySettingsResponse = await configurationProvider.GetGoogleSettingAsync();
            var settings = cloudinarySettingsResponse.Result;

            return settings;
        }


        public async Task<UserCredential> LoginAsync() {
            var clientSecrets = new ClientSecrets {
                ClientId = _googleConfig.ClientId,
                ClientSecret = _googleConfig.ClientSecret
            };

            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets,
                scopes,
                "user",
                CancellationToken.None
            );
        }
    }

}
