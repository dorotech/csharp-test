using DoroTech.BookStore.Application.Common;
using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Contracts.Requests.Queries.Auth;
using DoroTech.BookStore.Contracts.Responses.Auth;
using DoroTech.BookStore.Domain.Aggregates;
using MapsterMapper;
using OperationResult;

namespace DoroTech.BookStore.Application.RequestHandlers.QueryHandlers;

public class LoginQueryHandler : BaseQueryHandler<LoginQuery, Result<AuthenticationResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public LoginQueryHandler(IUserRepository userRepository, IMapper mapper, IPasswordEncrypter passwordEncrypter, IJwtTokenGenerator jwtTokenGenerator)
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
            return Result.Error<AuthenticationResponse>(new Exception("User does not exists"));

        if (!_passwordEncrypter.VerifyPassword(request.Password, user.Hash))
            return Result.Error<AuthenticationResponse>(new Exception("Incorrect credentials"));

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
