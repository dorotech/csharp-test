using AutoMapper;
using Bookstore.Domain.Commands.v1.Authentication;
using Bookstore.Domain.Contracts.v1.Repositories;
using Bookstore.Domain.Dtos.v1.Request.Authentication;
using Bookstore.Domain.Enums.v1;
using Bookstore.Domain.Mappings.v1.Profiles;
using Bookstore.Infrastructure.Data;
using Bookstore.Infrastructure.Data.Repositories.v1;
using Bookstore.Infrastructure.Security;
using MediatR;

namespace Bookstore.Domain.Tests.Commands
{
    public class AuthenticationCommandsTests
    {
        private BookstoreContext bookstoreContext;
        private IUserRepository userRepository;
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            bookstoreContext = DatabaseHelpers.GetInMemoryBookstoreContext();
            userRepository = new UserRepository(bookstoreContext);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new UserProfile()));
            mapper = new Mapper(configuration);
        }

        [TearDown]
        public async Task Teardown()
        {
            if (bookstoreContext is not null)
                await bookstoreContext.DisposeAsync();
        }

        [Test]
        public async Task RegisterUserCommandHandler_HandleCommand_Should_Return_User()
        {
            Unit unit = await RegisterUserCommandHandler();

            unit.Should().NotBeNull();
        }

        [Test]
        public async Task GetUserTokenCommandHandler_HandleCommand_Should_Return_User()
        {
            await RegisterUserCommandHandler(new RegisterUserDto("GetUserTokenCommandHandler@test.com", "123", "GetUserTokenCommandHandler", Role.administrator));

            var getUserTokenCommandHandler = new GetUserTokenCommandHandler(userRepository
            , mapper, new Cryptography(CryptographyHelpers.GetCryptographyKeys()));

            var getUserTokenDto = new GetUserTokenDto("GetUserTokenCommandHandler@test.com", "123");
            var getUserTokenCommand = new GetUserTokenCommand(getUserTokenDto);

            var user = await getUserTokenCommandHandler.Handle(getUserTokenCommand, CancellationToken.None);

            user.Should().NotBeNull();
        }

        private async Task<Unit> RegisterUserCommandHandler(RegisterUserDto? registerUserDto = null)
        {
            var registerUserCommandHandler = new RegisterUserCommandHandler(userRepository
            , mapper, new Cryptography(CryptographyHelpers.GetCryptographyKeys()));

            registerUserDto ??= new RegisterUserDto("test@test.com", "123", "Test", Role.administrator);
            var registerUserCommand = new RegisterUserCommand(registerUserDto);

            var unit = await registerUserCommandHandler.Handle(registerUserCommand, CancellationToken.None);
            return unit;
        }

    }
}