using AMAK.Application.Common.Helpers;
using AMAK.Application.Services.Product.Common;

namespace AMAK.Application.Services.Product.Queries.Search {
    public class SearchProductQuery : PaginationResponse<List<ProductResponse>> {
        public string? Name { get; set; }

        public SearchProductQuery(string? name) {
            Name = name;
        }
    }
}