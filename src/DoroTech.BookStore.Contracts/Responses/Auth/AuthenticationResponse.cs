namespace DoroTech.BookStore.Contracts.Responses.Auth;

public record AuthenticationResponse(
    long Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);