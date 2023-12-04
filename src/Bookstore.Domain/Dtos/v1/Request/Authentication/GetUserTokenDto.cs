namespace Bookstore.Domain.Dtos.v1.Request.Authentication;

public record GetUserTokenDto(string Email, string Password);