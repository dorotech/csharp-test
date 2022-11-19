using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dorotec_backend_test.Classes.DTOs;

public class BookDTO
{
    public BookDTO()
    { }

    [JsonIgnore]
    public int? Id { get; set; }
    public int? Price { get; set; }
    [MaxLength(123)]
    public string? Name { get; set; }
    [MaxLength(123)]
    public string? Author { get; set; }
    [MaxLength(63)]
    public string? Genre { get; set; }
    public int? Edition { get; set; }
    public int? Pages { get; set; }
}
