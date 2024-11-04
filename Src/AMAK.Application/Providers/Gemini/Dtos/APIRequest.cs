
namespace AMAK.Application.Providers.Gemini.Dtos {
    public class AiRequest<T> {
        public required T Prompt { get; set; }
    }
}