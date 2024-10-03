
namespace AMAK.Application.Services.AI.Dtos {
    public class AIRequest<T> {
        public required T Prompt { get; set; }
    }
}