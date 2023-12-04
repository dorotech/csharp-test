using System.Security.Cryptography;
using System.Text;

namespace Bookstore.Infrastructure.Security;

public class Cryptography(CryptographyKeys cryptographyKeys)
{
    public async Task<string> EncryptAsync(string senha)
    {
        using var aes = Aes.Create();

        aes.Key = Encoding.UTF8.GetBytes(cryptographyKeys.Key);
        aes.IV = Encoding.UTF8.GetBytes(cryptographyKeys.IV);

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        await using var ms = new MemoryStream();
        await using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        var sw = new StreamWriter(cs);

        await sw.WriteAsync(senha);
        await sw.FlushAsync();
        await sw.DisposeAsync();
        await cs.FlushAsync();
        await ms.FlushAsync();

        return Convert.ToBase64String(ms.ToArray());
    }
}
