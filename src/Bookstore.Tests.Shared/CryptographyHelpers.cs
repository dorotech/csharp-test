using Bookstore.Infrastructure.Security;

namespace Bookstore.Tests.Shared;

public class CryptographyHelpers
{
    public static CryptographyKeys GetCryptographyKeys() 
        => new("b6443e99308554098994475793478816", "y38j#j3&-Y(495`|");
}