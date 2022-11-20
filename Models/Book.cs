using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dorotec_backend_test.Models;

[Table("book")]
[Index(nameof(Author), nameof(Name), nameof(Genre), nameof(Edition), IsUnique = true)]
public class Book
{
    public Book()
    { }

    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("price")]
    public int Price { get; set; }

    [Column("name")]
    [MaxLength(123)]
    public string Name { get; set; }
    [Column("author")]
    [MaxLength(123)]
    public string Author { get; set; }

    [Column("genre")]
    [MaxLength(63)]
    public string Genre { get; set; }

    [Column("edition")]
    public int Edition { get; set; }

    [Column("pages")]
    public int Pages { get; set; }

    [Column("publish_date")]
    public DateTime PublishDate { get; set; }
}
