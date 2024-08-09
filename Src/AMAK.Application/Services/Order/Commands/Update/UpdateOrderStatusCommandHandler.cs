using AMAK.Application.Common.Exceptions;
using AMAK.Application.Common.Helpers;
using AMAK.Application.Interfaces;
using MediatR;
using System.Net;

namespace AMAK.Application.Services.Order.Commands.Update {
    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Response<string>> {

        private readonly IRepository<Domain.Models.Order> _orderStatusRepository;

        public UpdateOrderStatusCommandHandler(IRepository<Domain.Models.Order> orderStatusRepository) {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<Response<string>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken) {

            var existingOrder = await _orderStatusRepository.GetById(request.Id) ?? throw new NotFoundException("Order not found!");

            switch (request.Data.Status) {
                case Domain.Enums.EOrderStatus.CREATE:
                    if (existingOrder.Status != Domain.Enums.EOrderStatus.PENDING) {
                        throw new BadRequestException("Can't update to CREATE status from the current status!");
                    }
                    existingOrder.Status = request.Data.Status;
                    await _orderStatusRepository.SaveChangesAsync();
                    break;

                case Domain.Enums.EOrderStatus.CANCEL:
                    if (existingOrder.Status != Domain.Enums.EOrderStatus.PENDING) {
                        throw new BadRequestException("Can't update to CANCEL status from the current status!");
                    }
                    existingOrder.Status = request.Data.Status;
                    await _orderStatusRepository.SaveChangesAsync();
                    break;

                case Domain.Enums.EOrderStatus.SHIPPING:
                    if (existingOrder.Status != Domain.Enums.EOrderStatus.CREATE) {
                        throw new BadRequestException("Can't update to SHIPPING status from the current status!");
                    }
                    existingOrder.Status = request.Data.Status;
                    await _orderStatusRepository.SaveChangesAsync();
                    break;

                case Domain.Enums.EOrderStatus.SUCCESS:
                    if (existingOrder.Status != Domain.Enums.EOrderStatus.SHIPPING) {
                        throw new BadRequestException("Can't update to SUCCESS status from the current status!");
                    }
                    existingOrder.Status = request.Data.Status;
                    await _orderStatusRepository.SaveChangesAsync();
                    break;

                default:
                    throw new BadRequestException("Invalid order status!");
            }


            return new Response<string>(HttpStatusCode.OK, "Update Success");

        }
    }
}