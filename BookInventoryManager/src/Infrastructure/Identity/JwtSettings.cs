namespace Infrastructure.Identity;

public class JwtSettings
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string Key { get; set; }
    
    public int ExpirationInSeconds { get; set; }
}