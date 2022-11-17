namespace dorotec_backend_test.Classes.DTOs;

public class BookDTO
{
    public BookDTO()
    { }

    public int? Id { get; set; }
    public int? Price { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    public int? Edition { get; set; }
    public int? Pages { get; set; }
}
