using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.Model
{
    public class Book
    {

        public Book()
        {
            title = string.Empty;
            decription = string.Empty;
            Author = string.Empty;
            title = string.Empty;
            isnb = string.Empty;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string decription { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int idCategory { get; set; }
        [Required]
        public string isnb { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public int idPublisher { get; set; }

    }
}