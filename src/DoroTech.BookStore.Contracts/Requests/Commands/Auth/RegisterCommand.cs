using DoroTech.BookStore.Contracts.Responses.Auth;
using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Commands.Auth;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : ICommand<Result<AuthenticationResponse>>;
