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
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}