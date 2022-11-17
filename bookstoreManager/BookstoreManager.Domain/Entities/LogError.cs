using System.ComponentModel.DataAnnotations;

namespace BookstoreManager.Domain.Entities
{
    public class LogError
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = String.Empty;
        public int? UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public bool Visualized { get; set; }
    }
}
