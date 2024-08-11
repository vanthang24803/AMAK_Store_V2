using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Payment.Dtos;
using MediatR;

namespace AMAK.Application.Services.Payment.Commands.Momo {
    public class HandlerMomoRequestCommand(HandlerMomoRequest data) : IRequest<Response<string>> {
        public HandlerMomoRequest Data { get; set; } = data;
    }
}