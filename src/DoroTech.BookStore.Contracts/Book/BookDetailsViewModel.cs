namespace DoroTech.BookStore.Contracts.Book;

public record struct BookDetailsViewModel
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int Edition { get; init; }
    public string Language { get; init; }
    public DateOnly PublicationDate { get; init; }
    public decimal? Cust { get; init; }
    public bool ItIsFromDonation { get; init; }
    public decimal SalePrice { get; init; }
    public string Isbn { get; init; }
    public int? Pages { get; init; }
}
