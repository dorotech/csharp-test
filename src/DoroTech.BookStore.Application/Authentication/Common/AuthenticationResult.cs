namespace DoroTech.BookStore.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);
