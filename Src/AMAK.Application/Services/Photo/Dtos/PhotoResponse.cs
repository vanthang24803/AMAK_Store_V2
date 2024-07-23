namespace AMAK.Application.Services.Photo.Dtos {
    public record PhotoResponse(
        Guid Id,
        string Url,
        DateTime CreateAt);
}