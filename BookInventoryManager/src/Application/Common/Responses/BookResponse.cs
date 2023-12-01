using Domain.Entities;

namespace Application.Common.Responses;

public class BookResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Edition { get; set; }
    public string Language { get; set; }
    public DateTime PublicationDate { get; set; }
    public PublisherResponse Publisher { get; set; }
    public CategoryResponse Category { get; set; }
    public AuthorResponse Author { get; set; }
    public decimal SalePrice { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public int Isbn { get; set; }
    public int CurrentInventory { get; set; }
    public bool Active { get; set; }
    public int? Pages { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookResponse>();
        }
    }
}