using DoroTech.BookStore.Contracts.Responses.Auth;
using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Commands.Auth;

public record LoginQuery(
    string Email,
    string Password
) : ICommand<Result<AuthenticationResponse>>;
