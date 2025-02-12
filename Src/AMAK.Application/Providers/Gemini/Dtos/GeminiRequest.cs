namespace AMAK.Application.Providers.Gemini.Dtos {
    public class GeminiRequest {
        public class Content {
            public List<Part> Parts { get; set; } = [];
        }
        public class Part {
            public string Text { get; set; } = null!;
        }

        public class Root {
            public List<Content> Contents { get; set; } = [];
        }
    }
}