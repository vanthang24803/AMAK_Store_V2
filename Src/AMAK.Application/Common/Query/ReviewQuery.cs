namespace AMAK.Application.Common.Query {
    public class ReviewQuery : BaseQuery {
        public string? Status { get; set; } = null;

        public string? Star { get; set; }
    }
}