namespace dorotec_backend_test.Classes.Exceptions;

public class UnauthorizedRequestException : Exception
{
    public UnauthorizedRequestException()
    {
    }

    public UnauthorizedRequestException(string? message) : base(message)
    {
    }

    public UnauthorizedRequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
