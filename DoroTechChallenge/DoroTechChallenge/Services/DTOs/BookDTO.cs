using DoroTechChallenge.Models;

namespace DoroTechChallenge.Services.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public List<string> PublishingCompanies { get; set; }

    public BookDTO(Book entity)
    {
        Id = entity.Id;
        Title = entity.Title;
        Description = entity.Description;
        PublishedDate = entity.PublishedDate;
        Author = entity.Author.AuthorName;
        Genre = entity.Genre.GenreName;
        PublishingCompanies = entity.PublishingCompanies
            .Select(x => x.CompanyName)
            .ToList();
    }
}
