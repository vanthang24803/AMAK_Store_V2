using AMAK.Domain.Models;
using Newtonsoft.Json;

namespace AMAK.Application.Providers.Mail.Dtos {
    public class OrderMailEvent {
        public string To { get; set; } = null!;
        public string Subject { get; set; } = null!;
        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        public Order Order { get; set; } = null!;

        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        public List<OrderDetail> OrderResponses { get; set; } = [];
    }
}