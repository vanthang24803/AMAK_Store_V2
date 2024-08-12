
using System.Text.Json.Serialization;
using AMAK.Domain.Enums;

namespace AMAK.Application.Services.Payment.Dtos {
    public class HandlerMomoRequest {
        public Guid OrderId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EMomoResultCode ResultCode { get; set; }
    }
}