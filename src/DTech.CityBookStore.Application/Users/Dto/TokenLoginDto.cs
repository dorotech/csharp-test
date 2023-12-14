using System.ComponentModel.DataAnnotations;

namespace DTech.CityBookStore.Application.Users.Dto;

public class TokenLoginDto
{
    [Required(ErrorMessage = "Login is required.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "SECRET_KEY is required.")]
    public string Password { get; set; }
}
