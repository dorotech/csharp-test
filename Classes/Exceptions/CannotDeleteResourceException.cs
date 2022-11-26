namespace dorotec_backend_test.Classes.Exceptions;

public class CannotDeleteResourceException : Exception
{
    public CannotDeleteResourceException()
    {
    }

    public CannotDeleteResourceException(string? message) : base(message)
    {
    }

    public CannotDeleteResourceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
