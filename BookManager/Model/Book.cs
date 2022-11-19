using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.Model
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? title { get; set; }
        public string? decription { get; set; }
        public string? idAuthor { get; set; }
        public int idCategory { get; set; }
        public string? isnb { get; set; }
        public int year { get; set; }
        public int idPublisher { get; set; }

    }
}