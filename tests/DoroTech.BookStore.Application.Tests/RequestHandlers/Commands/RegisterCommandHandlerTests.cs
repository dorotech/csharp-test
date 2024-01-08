namespace DoroTech.BookStore.Application.Tests.RequestHandlers.Commands;

public class RegisterCommandHandlerTests
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUserRepository _userRepository;
    private readonly RegisterCommandHandler _sut;

    public RegisterCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordEncrypter = Substitute.For<IPasswordEncrypter>();
        _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _sut = new RegisterCommandHandler(_jwtTokenGenerator, _userRepository, _passwordEncrypter);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenUserIsCreated()
    {
        // Arrange
        var command = new RegisterCommand("Admin", "Root", "admin@bookstore.com", "admin123");

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result
            .Should()
            .NotBeNull()
            .And
            .BeOfType<Result<AuthenticationResponse>>();
    }
}
