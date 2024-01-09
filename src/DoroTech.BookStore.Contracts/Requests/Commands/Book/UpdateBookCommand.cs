using DoroTech.BookStore.Contracts.Responses.Book;
using Microsoft.AspNetCore.Mvc;
using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Commands;

public record UpdateBookCommand : ICommand<Result<BookDetailsViewModel>>
{
    [FromRoute(Name = "id")]
    public long Id { get; init; }

    [FromBody]
    public UpdateBookDto BookDetails { get; set; } = null!;
}

public record UpdateBookDto
{
    public string Title { get; init; }
    public string? Author { get; init; }
    public string? Description { get; init; }
    public int? Edition { get; init; }
    public string? Language { get; init; }
    public DateOnly? PublicationDate { get; init; }
    public decimal? Cust { get; init; }
    public bool? ItIsFromDonation { get; init; }
    public decimal? Price { get; init; }
    public string? Isbn { get; init; }
    public int? Pages { get; init; }
}