using CrossCutting.Models;

namespace Application.Features.Users.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<ReturnMessage<SignInResponse>>;
