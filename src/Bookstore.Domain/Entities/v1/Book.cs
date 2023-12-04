namespace Bookstore.Domain.Entities.v1;

public class Book
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int Year { get; set; }
    public string? Genre { get; set; }
    public string? Publisher { get; set; }
    public int Pages { get; set; }
    public string? Status { get; set; }
}