using Bookstore.Domain.Dtos.v1.Request.Authentication;

namespace Bookstore.Domain.Commands.v1.Authentication;

public record GetUserTokenCommand(GetUserTokenDto GetUserTokenDto) : Command<User?>;