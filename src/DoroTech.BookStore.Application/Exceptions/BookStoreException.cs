using Microsoft.AspNetCore.Http;

namespace DoroTech.BookStore.Application.Exceptions;

public class BookStoreException : Exception
{
    public BookStoreException(string resourceTitle, int statusCode = StatusCodes.Status400BadRequest, string? resourceDetail = null) : base()
    {
        ResourceTitle = resourceTitle;
        StatusCode = statusCode;
        ResourceDetail = resourceDetail;
    }
    public string Type { get; set; }
    public string ResourceTitle { get; set; }
    public string? ResourceDetail { get; set; }
    public int StatusCode { get; set; }
}
