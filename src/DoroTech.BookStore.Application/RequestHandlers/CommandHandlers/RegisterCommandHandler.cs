using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;

public class RegisterCommandHandler : BaseCommandHandler<RegisterCommand, Result<AuthenticationResponse>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncrypter _passwordEncrypter;


    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordEncrypter passwordEncrypter)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordEncrypter = passwordEncrypter;
    }

    public override async Task<Result<AuthenticationResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Result.Error<AuthenticationResponse>(new Exception("error"));

        var passwordHash = _passwordEncrypter.CreatePasswordHash(command.Password);

        var user = User.Create(command.FirstName, command.LastName, command.Email, passwordHash);

        bool verified = _passwordEncrypter.VerifyPassword(command.Password, user.Hash);
        if (!verified)
            return Result.Error<AuthenticationResponse>(new Exception(""));

        _userRepository.Add(user);

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
