namespace Domain.ValueObjects;

public class AuthToken
{
    public string Token { get; }
    public int ExpiresIn { get; }

    public AuthToken(string token, int expiresIn)
    {
        Token = token;
        ExpiresIn = expiresIn;
    }
}
