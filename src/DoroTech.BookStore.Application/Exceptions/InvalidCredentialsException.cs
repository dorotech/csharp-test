using Microsoft.AspNetCore.Http;

namespace DoroTech.BookStore.Application.Exceptions;


public class InvalidCredentialsException : BookStoreException
{
    public InvalidCredentialsException(string resourceTitle) : base(resourceTitle, StatusCodes.Status400BadRequest, "Email or password are invalids") { }
}
