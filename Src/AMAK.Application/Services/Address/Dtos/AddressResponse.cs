namespace AMAK.Application.Services.Address.Dtos {
    public record AddressResponse(
        Guid Id,
        string Name,
        bool IsActive,
        DateTime CreateAt
    );
}