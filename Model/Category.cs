using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Column(TypeName = "varchar(250)")]
        public string Name { get; set; }
        public virtual List<Book> Books { get; set; }
    }
}
