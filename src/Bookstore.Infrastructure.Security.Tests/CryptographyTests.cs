using Bookstore.Tests.Shared;

namespace Bookstore.Infrastructure.Security.Tests;

public class CryptographyTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Encrypt_Should_Return_Encrypted_Text()
    {
        var cryptography = new Cryptography(CryptographyHelpers.GetCryptographyKeys());

        var encryptedText = await cryptography.EncryptAsync("testpassword");

        encryptedText.Should().Be("JrG8J/Pxao9wLwnQofqN8A==");
    }
}