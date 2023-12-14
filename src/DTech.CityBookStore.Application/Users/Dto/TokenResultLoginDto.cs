namespace DTech.CityBookStore.Application.Users.Dto;

public class TokenResultLoginDto
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public TokenDataDto UsuarioToken { get; set; }
}
