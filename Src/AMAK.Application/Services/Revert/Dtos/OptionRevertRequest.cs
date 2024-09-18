namespace AMAK.Application.Services.Revert.Dtos {
    public class OptionRevertRequest {
        public List<OptionRevert> Reverts { get; set; } = [];
    }

    public class OptionRevert {
        public Guid Id { get; set; }
    }
}