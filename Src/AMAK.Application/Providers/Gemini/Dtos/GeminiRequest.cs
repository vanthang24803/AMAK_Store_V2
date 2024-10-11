namespace AMAK.Application.Providers.Gemini.Dtos {
    public class GeminiRequest {
        public class Content {
            public string Role { get; set; } = "user";
            public List<Part> Parts { get; set; } = [];
        }

        public class GenerationConfig {
            public int Temperature { get; set; } = 1;
            public int TopK { get; set; } = 64;
            public double TopP { get; set; } = 0.95;
            public int MaxOutputTokens { get; set; } = 8192;
            public string ResponseMimeType { get; set; } = "text/plain";
        }

        public class Part {
            public string Text { get; set; } = null!;
        }

        public class Root {
            public List<Content> Contents { get; set; } = [];
            public GenerationConfig GenerationConfig { get; set; } = null!;
        }
    }
}