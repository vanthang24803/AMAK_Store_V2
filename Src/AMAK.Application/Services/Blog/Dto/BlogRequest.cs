using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AMAK.Application.Services.Blog.Dto {
    public class BlogRequest {
        [MaxLength(255, ErrorMessage = "Title <= 255 characters")]
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; } = null!;

        [NotNull]
        [Required(ErrorMessage = "Content Required")]

        public string Content { get; set; } = null!;
    }
}