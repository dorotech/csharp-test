using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.Model
{
    public class CustomLog
    {
        public CustomLog()
        {
            operation = string.Empty;
            trace = string.Empty;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string operation { get; set; }
        [Required]
        public string trace { get; set; }
        [Required]
        public DateTime createAt { get; set; }

    }
}