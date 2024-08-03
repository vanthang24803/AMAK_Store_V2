using AMAK.Application.Services.Address.Dtos;

namespace AMAK.Application.Services.Me.Dtos {
    public class ProfileResponse {

        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public IList<string> Roles { get; set; } = [];
        public List<AddressResponse> Addresses { get; set; } = [];
        public double TotalPrice { get; set; }
        public int TotalOrder { get; set; }
        public int ProcessOrder { get; set; }
        public string Rank { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}