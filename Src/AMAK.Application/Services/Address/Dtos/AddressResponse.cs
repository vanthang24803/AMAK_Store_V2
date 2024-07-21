namespace AMAK.Application.Services.Address.Dtos {
    public class AddressResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreateAT { get; set; }

    }
}