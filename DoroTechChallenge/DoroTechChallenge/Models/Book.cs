namespace DoroTechChallenge.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public string Author { get; set; }
    public string PublishingCompany { get; set; }
    public bool Active { get; set; }
    public DateTime PublishedAt { get; set; }
}
