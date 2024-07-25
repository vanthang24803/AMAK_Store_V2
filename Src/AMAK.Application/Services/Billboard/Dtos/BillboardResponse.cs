namespace AMAK.Application.Services.Billboard.Dtos {
    public record BillboardResponse(
        Guid Id,
        string Thumbnail,
        string Url,
        DateTime CreateAt
    );

}