namespace DoroTech.BookStore.Application.Exceptions;

public class BookNotFoundException : BookStoreException
{
    public BookNotFoundException(long id) : base("BOOK_NOT_FOUND", StatusCodes.Status404NotFound, $"Book id {id} not found")
    {
    }
}
