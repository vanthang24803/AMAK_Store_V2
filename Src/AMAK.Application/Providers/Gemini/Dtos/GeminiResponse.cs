namespace AMAK.Application.Providers.Gemini.Dtos {

    public class GeminiResponse {
        public List<Candidate> Candidates { get; set; } = null!;
        public UsageMetadata UsageMetadata { get; set; } = null!;
    }

    public class Candidate {
        public Content Content { get; set; } = null!;
        public string FinishReason { get; set; } = null!;
        public int Index { get; set; }
        public List<SafetyRating> SafetyRatings { get; set; } = [];
    }

    public class Content {
        public List<Part> Parts { get; set; } = [];
        public string Role { get; set; } = null!;
    }

    public class Part {
        public string Text { get; set; } = null!;
    }

    public class SafetyRating {
        public string Category { get; set; } = null!;
        public string Probability { get; set; } = null!;
    }

    public class UsageMetadata {
        public int PromptTokenCount { get; set; }
        public int CandidatesTokenCount { get; set; }
        public int TotalTokenCount { get; set; }
    }


}