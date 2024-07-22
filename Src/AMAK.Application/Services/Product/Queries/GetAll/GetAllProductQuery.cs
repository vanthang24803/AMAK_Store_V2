using AMAK.Application.Common.Helpers;
using AMAK.Application.Common.Query;
using AMAK.Application.Services.Product.Common;
using MediatR;

namespace AMAK.Application.Services.Product.Queries.GetAll {
    public class GetAllProductQuery : IRequest<PaginationResponse<List<ProductResponse>>> {
        public ProductQuery Query { get; set; } = null!;

        public GetAllProductQuery(ProductQuery query) {
            Query = query;
        }
    }
}