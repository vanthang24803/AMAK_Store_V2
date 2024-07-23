namespace AMAK.Application.Services.Categories.Common {
    public record CategoryResponse(
        Guid Id,
        string Name,
        DateTime CreateAt
    );
}