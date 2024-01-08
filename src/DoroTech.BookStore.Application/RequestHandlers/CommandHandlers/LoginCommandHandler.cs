using DoroTech.BookStore.Application.Common;
using DoroTech.BookStore.Application.Exceptions;
using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
using DoroTech.BookStore.Contracts.Responses.Auth;
using DoroTech.BookStore.Domain.Aggregates;
using MapsterMapper;
using OperationResult;

namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class LoginCommandHandler : BaseCommandHandler<LoginQuery, Result<AuthenticationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordEncrypter passwordEncrypter, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public override async Task<Result<AuthenticationResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(request.Email) is not User user)
            return Result.Error<AuthenticationResponse>(new UserDoesNotExistsException());

        if (!_passwordEncrypter.VerifyPassword(request.Password, user.Hash))
            return Result.Error<AuthenticationResponse>(new InvalidCredentialsException("INVALID_CREDENTIALS"));

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token
        );
    }
}
