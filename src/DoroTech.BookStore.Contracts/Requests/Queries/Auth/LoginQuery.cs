using DoroTech.BookStore.Contracts.Responses.Auth;
using OperationResult;

namespace DoroTech.BookStore.Contracts.Requests.Queries.Auth;

public record LoginQuery(
    string Email,
    string Password
) : IQuery<Result<AuthenticationResponse>>;
