using AMAK.Application.Common.Constants;

namespace AMAK.Application.Common.Query {
    public class ProductQuery : BaseQuery {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Action { get; set; }
        public string? SortBy { get; set; }
        public string? OrderBy { get; set; }
    }

}