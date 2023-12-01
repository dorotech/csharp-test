using System.Net;
using Application.Common.Interfaces;
using CrossCutting.Extensions;
using CrossCutting.Models;
using Domain.ValueObjects;

namespace Application.Features.Users.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, ReturnMessage<SignInResponse>>
{
    private readonly IAuthenticationService _authenticationService;

    public SignInCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<ReturnMessage<SignInResponse>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var authTokenresponse = await _authenticationService.SignIn(command.Email, command.Password);

            if (authTokenresponse.Data == null)
                return authTokenresponse.ParseOnlyErros<AuthToken, SignInResponse>();

            return new ReturnMessage<SignInResponse>(new SignInResponse(authTokenresponse.Data.Token, authTokenresponse.Data.ExpiresIn), authTokenresponse.StatusCode);
        }
        catch (Exception ex)
        {
            return new ReturnMessage<SignInResponse>(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}