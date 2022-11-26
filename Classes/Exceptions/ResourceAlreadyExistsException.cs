using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dorotec_backend_test.Classes.Exceptions;

public class ResourceAlreadyExistsException : Exception
{
    public ResourceAlreadyExistsException()
    {
    }

    public ResourceAlreadyExistsException(string? message) : base(message)
    {
    }

    public ResourceAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public static void Test(){}
}
