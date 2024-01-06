namespace DoroTech.BookStore.Contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password
);
