using System.ComponentModel.DataAnnotations;

namespace BookManager.Model
{
    public class Token
    {
        [Required]
        public string? token { get; set; }
        [Required]
        public DateTime expires { get; set; }
    }
}