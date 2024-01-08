using Microsoft.AspNetCore.Http;

namespace DoroTech.BookStore.Application.Exceptions;
internal class UserDoesNotExistsException : BookStoreException
{
    public UserDoesNotExistsException() : base("USER_NOT_FOUND", StatusCodes.Status400BadRequest, "User not found") { }
}
