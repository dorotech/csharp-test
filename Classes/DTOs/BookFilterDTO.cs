using System.ComponentModel.DataAnnotations;

namespace dorotec_backend_test.Classes.DTOs;

public class BookFilterDTO
{
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    [MaxLength(123)]
    public string? Name { get; set; }
    [MaxLength(123)]
    public string? Author { get; set; }
    [MaxLength(63)]
    public string? Genre { get; set; }
    public int? Edition { get; set; }
    public int? Pages { get; set; }
}
