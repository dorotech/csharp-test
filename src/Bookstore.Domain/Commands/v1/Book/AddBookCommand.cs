using Bookstore.Domain.Dtos.v1.Request.Book;

namespace Bookstore.Domain.Commands.v1.Book;

public record class AddBookCommand(AddBookDto Book) : Command<Guid>;