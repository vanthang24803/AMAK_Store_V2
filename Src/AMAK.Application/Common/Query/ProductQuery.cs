namespace AMAK.Application.Common.Query {
    public class ProductQuery : BaseQuery {
        public string? Name { get; init; }
        public string? Category { get; set; }
        public string? Action { get; set; }
        public string? SortBy { get; set; }
        public string? OrderBy { get; set; }
    }

}