
using AMAK.Application.Services.Order.Dtos;
using AMAK.Application.Services.Product.Common;

namespace AMAK.Application.Services.Search.Dtos {
    public class SearchResponse {
        public List<ProductResponse> Products { get; set; } = [];
        public List<OrderResponse> Orders { get; set; } = [];

    }
}