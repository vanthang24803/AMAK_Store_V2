namespace AMAK.Application.Services.Options.Dtos {
    public record OptionResponse(
        Guid Id,
        string Name,
        int Sale,
        double Quantity,
        bool IsActive,
        DateTime CreateAt);
}