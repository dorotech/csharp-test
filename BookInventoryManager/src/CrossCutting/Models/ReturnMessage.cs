using System.Net;

namespace CrossCutting.Models;

public class ReturnMessage<T> : ReturnMessage
{
    public T? Data { get; set; }

    public ReturnMessage(T? data, HttpStatusCode statusCode) : base(statusCode)
    {
        Data = data;
        StatusCode = (int)statusCode;
    }

    public ReturnMessage(T? data, int statusCode) : base(statusCode)
    {
        Data = data;
        StatusCode = statusCode;
    }

    public ReturnMessage(string errorMessage, int statusCode) : base(errorMessage, statusCode)
    {
    }

    public ReturnMessage(string errorMessage, HttpStatusCode statusCode) : base(errorMessage, statusCode)
    {
    }

    public ReturnMessage(List<string> errors, HttpStatusCode statusCode) : base(errors, statusCode)
    {
    }

    public ReturnMessage(List<string> errors, int statusCode) : base(errors, statusCode)
    {
    }
}

public class ReturnMessage
{
    public int StatusCode { get; set; }

    public bool IsSuccess => (Errors is null || !Errors.Any()) && ((StatusCode >= 200) && (StatusCode <= 299));

    public List<string> Errors { get; set; } = new List<string>();

    public ReturnMessage(HttpStatusCode statusCode)
    {
        StatusCode = (int)statusCode;
    }

    public ReturnMessage(int statusCode)
    {
        StatusCode = statusCode;
    }

    public ReturnMessage(string errorMessage, HttpStatusCode statusCode)
    {
        Errors = new List<string> { errorMessage };
        StatusCode = (int)statusCode;
    }

    public ReturnMessage(string errorMessage, int statusCode)
    {
        Errors = new List<string> { errorMessage };
        StatusCode = statusCode;
    }

    public ReturnMessage(List<string> errors, HttpStatusCode statusCode)
    {
        Errors = errors;
        StatusCode = (int)statusCode;
    }

    public ReturnMessage(List<string> errors, int statusCode)
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}