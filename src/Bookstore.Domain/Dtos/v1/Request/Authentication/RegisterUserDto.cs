using Bookstore.Domain.Enums.v1;

namespace Bookstore.Domain.Dtos.v1.Request.Authentication;

public record RegisterUserDto(string Email, string Password, string Name, Role Role);