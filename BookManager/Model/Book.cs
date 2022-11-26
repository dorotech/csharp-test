using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.Model
{
    public class Book
    {

        public Book()
        {
            name = string.Empty;
            decription = string.Empty;
            author = string.Empty;
            isnb = string.Empty;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string decription { get; set; }
        [Required]
        public string author { get; set; }
        [Required]
        public int idCategory { get; set; }
        [Required]
        public string isnb { get; set; }
        [Required]
        public int year { get; set; }
        [Required]
        public int idPublisher { get; set; }
        [Required]
        public DateTime createAt { get; set; }

        [Required]
        public int exemplary { get; set; }



    }
}