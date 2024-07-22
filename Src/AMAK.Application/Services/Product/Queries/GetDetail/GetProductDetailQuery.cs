using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;
using MediatR;

namespace AMAK.Application.Services.Product.Queries.GetDetail {
    public class GetProductDetailQuery : IRequest<Response<ProductDetailResponse>> {
        public Guid Id { get; set; }

        public GetProductDetailQuery(Guid id) {
            Id = id;
        }
    }
}