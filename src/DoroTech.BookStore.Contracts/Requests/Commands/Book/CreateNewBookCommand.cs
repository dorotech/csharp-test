using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Commands;

public sealed record CreateNewBookCommand : ICommand<Result<BookDetailsViewModel>>
{
    public string Title { get; init; } = null!;
    public string Author { get; init; } = null!;
    public string? Description { get; init; }
    public int Edition { get; init; }
    public string Language { get; init; } = null!;
    public DateOnly PublicationDate { get; init; }
    public decimal? Cust { get; init; }
    public bool ItIsFromDonation { get; init; }
    public decimal Price { get; init; }
    public string Isbn { get; init; } = null!;
    public int? Pages { get; init; }
}
