namespace AMAK.Application.Services.Revert.Dtos {
    public class ProductRevertRequest {
        public List<ProductRevert> Reverts { get; set; } = [];
    }

    public class ProductRevert {
        public Guid Id { get; set; }
    }
}