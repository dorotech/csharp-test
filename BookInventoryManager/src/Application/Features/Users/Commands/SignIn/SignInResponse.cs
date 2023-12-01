namespace Application.Features.Users.Commands.SignIn;

public record SignInResponse(string Token, int ExpiresInd);