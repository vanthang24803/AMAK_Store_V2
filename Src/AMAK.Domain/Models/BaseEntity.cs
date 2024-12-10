using System.ComponentModel.DataAnnotations;

namespace AMAK.Domain.Models {
    public abstract class BaseEntity<TPrimaryKey> where TPrimaryKey : struct {
        [Key]
        public required TPrimaryKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        public DateTime DeleteAt { get; set; }
    }
}