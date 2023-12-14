using System.Text;

namespace DTech.CityBookStore.Application.Extensions;

public static class CryptoExtensions
{
    public static string Encrypt(this string input)
    {
        using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}
