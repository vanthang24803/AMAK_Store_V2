namespace AMAK.Application.Services.Blog.Dto {
    public class BlogResponse {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Thumbnail { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public Author Author { get; set; } = null!;

    }

    public class Author {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}