using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.Tests.RequestHandlers.Commands;

public class LoginQueryHandlerTests : MapperServiceFactory
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly LoginCommandHandler _sut;

    public LoginQueryHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordEncrypter = Substitute.For<IPasswordEncrypter>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _sut = new LoginCommandHandler(_userRepository, _mapper, _passwordEncrypter, _jwtTokenGenerator);
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

        _jwtTokenGenerator
            .GenerateToken(user)
            .Returns("token");

        //Act
        var result = await _sut.Handle(query, CancellationToken.None);

        //Assert
        result
            .Value
            .Should()
            .NotBeNull()
            .And
            .BeOfType<AuthenticationResponse>();

        result
            .Value.As<AuthenticationResponse>()
            .Token.Should().NotBeNullOrEmpty();
    }
}
