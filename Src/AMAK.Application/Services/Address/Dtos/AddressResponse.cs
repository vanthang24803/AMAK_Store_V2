namespace AMAK.Application.Services.Address.Dtos {
    public record AddressResponse(
        Guid Id,
        string AddressName,
        string FirstName,
        string LastName,
        string NumberPhone,
        bool IsActive,
        DateTime CreateAt
    );
}