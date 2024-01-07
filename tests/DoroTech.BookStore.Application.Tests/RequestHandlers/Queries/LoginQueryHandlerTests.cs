using DoroTech.BookStore.Application.Common;
using DoroTech.BookStore.Application.Repositories;
using DoroTech.BookStore.Application.RequestHandlers.QueryHandlers;
using DoroTech.BookStore.Contracts.Requests.Queries.Auth;
using DoroTech.BookStore.Contracts.Responses.Auth;
using DoroTech.BookStore.Domain.Aggregates;
using FluentAssertions;
using MapsterMapper;
using NSubstitute;

namespace DoroTech.BookStore.Application.Tests.RequestHandlers.Queries;

public class LoginQueryHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly LoginQueryHandler _sut;

    public LoginQueryHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _passwordEncrypter = Substitute.For<IPasswordEncrypter>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _sut = new LoginQueryHandler(_userRepository, _mapper, _passwordEncrypter, _jwtTokenGenerator);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenUserLogin()
    {
        // Arrange
        var query = new LoginQuery("admin@email.com", "Teste@123");

        var user = User.Create("Admin", "Root", query.Email, "", "");
        _userRepository
            .GetUserByEmail(query.Email)
            .Returns(user);

        _passwordEncrypter
            .VerifyPassword(Arg.Any<string>(), Arg.Any<string>())
            .Returns(true);

        //Act
        var result = await _sut.Handle(query, CancellationToken.None);

        //Assert
        result
            .Value
            .Should()
            .NotBeNull()
            .And
            .BeOfType<AuthenticationResponse>();
    }
}
