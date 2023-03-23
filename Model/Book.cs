using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "{0} is required!")]
        [Column(TypeName = "varchar(250)")]
        public string Title { get; set; }


        [Required(ErrorMessage = "{0} is required!")]
        [Column(TypeName = "varchar(300)")]
        public string Description { get; set; }


        [Required(ErrorMessage = "{0} is required!")]
        [Column(TypeName = "varchar(100)")]
        public string Author { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
