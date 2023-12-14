namespace DTech.CityBookStore.Application.Users.Dto;

public class TokenDataDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<TokenClaimDto> Claims { get; set; }
}
