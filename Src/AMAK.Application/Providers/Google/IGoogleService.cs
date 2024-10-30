using Google.Apis.Auth.OAuth2;

namespace AMAK.Application.Providers.Google {
    public interface IGoogleService {
        Task<UserCredential> LoginAsync();
    }
}