using System.ComponentModel.DataAnnotations.Schema;

namespace AMAK.Domain.Models;

public class Conversation : BaseEntity<Guid>
{
    public string Message { get; set; } = null!;
    public string UserId { get; set; } = null!;

    public bool IsBotReply { get; set; }

    [ForeignKey(nameof(UserId))] public ApplicationUser? User { get; set; }
}