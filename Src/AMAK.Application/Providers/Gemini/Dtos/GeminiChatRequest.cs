using System.ComponentModel.DataAnnotations;

namespace AMAK.Application.Providers.Gemini.Dtos;

public class GeminiChatRequest
{
    [Required]
    [MinLength(1)]
    public string Message { get; set; } = null!;
}