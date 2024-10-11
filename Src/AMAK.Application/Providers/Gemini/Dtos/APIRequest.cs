
namespace AMAK.Application.Providers.Gemini.Dtos {
    public class AIRequest<T> {
        public required T Prompt { get; set; }
    }
}