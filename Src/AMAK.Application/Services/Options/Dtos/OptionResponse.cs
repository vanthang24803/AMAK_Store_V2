namespace AMAK.Application.Services.Options.Dtos {
    public record OptionResponse(
        Guid Id,
        string Name,
        int Sale,
        double Price,
        double Quantity,
        bool IsActive,
        DateTime CreateAt);
}