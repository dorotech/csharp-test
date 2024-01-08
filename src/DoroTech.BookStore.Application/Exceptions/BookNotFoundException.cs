namespace DoroTech.BookStore.Application.Exceptions;

internal class BookNotFoundException : BookStoreException
{
    public BookNotFoundException(string? title) : base("BOOK_NOT_FOUND", StatusCodes.Status404NotFound, $"Book { title ?? string.Empty } not found")
    {
    }
}
