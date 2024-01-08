using Microsoft.AspNetCore.Http;

namespace DoroTech.BookStore.Application.Exceptions;

public class BookTitleAlreadyExistException : BookStoreException
{
    public BookTitleAlreadyExistException(string title) : base("BOOK_ALREADY_EXISTS", StatusCodes.Status409Conflict, $"Book {title} already exists")
    {
    }
}
