namespace DTech.CityBookStore.Api.Configurations;

public class IdentityConfiguration
{
    public const string CONFIGURATION_SECTION = "Identity";
    public string PublicKeyFilePath { get; set; }
    public int ExpirationInHours { get; set; } = 8;
    public string ValidateAt { get; set; }
    public string Issuer { get; set; }
}