using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Order.Dtos {
    public class UpdateStatusOrderRequest {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EOrderStatus Status { get; set; }
    }
}