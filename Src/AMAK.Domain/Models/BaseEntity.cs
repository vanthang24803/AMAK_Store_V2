using System.ComponentModel.DataAnnotations;

namespace AMAK.Domain.Models {
    public class BaseEntity<TPrimaryKey> where TPrimaryKey : struct {
        [Key]
        public required TPrimaryKey Id { get; set; } 
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}