namespace AMAK.Application.Services.Category.Dtos {
    public class CategoryResponse {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreateAt { get; set; }
    }
}