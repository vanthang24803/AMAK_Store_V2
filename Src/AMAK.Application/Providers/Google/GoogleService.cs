using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;

namespace AMAK.Application.Providers.Google {

    public class GoogleService : IGoogleService {
        private readonly GoogleSettings _googleConfig;
        private readonly string[] scopes = [
            "https://www.googleapis.com/auth/gmail.readonly",
            "https://www.googleapis.com/auth/userinfo.email"
        ];

        public GoogleService(IOptions<GoogleSettings> options) {
            _googleConfig = options.Value;
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
