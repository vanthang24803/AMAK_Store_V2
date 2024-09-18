namespace AMAK.Application.Services.Revert.Dtos {
    public class MediaRevertRequest {
        public List<MediaRevert> Reverts { get; set; } = [];
    }

    public class MediaRevert {
        public Guid Id { get; set; }
    }
}