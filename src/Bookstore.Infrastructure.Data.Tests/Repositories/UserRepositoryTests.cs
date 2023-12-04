using Bookstore.Domain.Entities.v1;
using Bookstore.Domain.Enums.v1;
using Bookstore.Infrastructure.Data.Repositories.v1;

namespace Bookstore.Infrastructure.Data.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private BookstoreContext bookstoreContext;
        private UserRepository userRepository;

        [SetUp]
        public void Setup()
        {
            bookstoreContext = DatabaseHelpers.GetInMemoryBookstoreContext();
            userRepository = new UserRepository(bookstoreContext);
        }

        [TearDown]
        public async Task Teardown()
        {
            if(bookstoreContext is not null)
                await bookstoreContext.DisposeAsync();
        }

        [Test]
        public async Task RegisterUserAsync_Should_Return_Id()
        {
            var book = await RegisterUserAsync();

            book.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAsync_Should_Return_UserByEmailAndPassword()
        {
            var userRegistered = await RegisterUserAsync();

            var user = await userRepository.GetAsync(userRegistered.Email!, userRegistered.Password!);

            user.Should().NotBeNull()
            .And
            .Match<User>(x => x.Email!.Equals(userRegistered.Email, StringComparison.OrdinalIgnoreCase))
            .And
            .Match<User>(x => x.Password!.Equals(userRegistered.Password, StringComparison.OrdinalIgnoreCase));

            user!.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task GetAsync_Should_Return_UserByEmail()
        {
            var userRegistered = await RegisterUserAsync();

            var user = await userRepository.GetAsync(userRegistered.Email!);

            user.Should().NotBeNull()
            .And
            .Match<User>(x => x.Email!.Equals(userRegistered.Email, StringComparison.OrdinalIgnoreCase));

            user!.Id.Should().NotBeEmpty();
        }

        private async Task<User> RegisterUserAsync()
        {
            var user = new User
            {
                Email = "test@test.com",
                Name = "Test",
                Password = "123",
                Role = Role.administrator
            };

            await userRepository.RegisterUserAsync(user);

            return user;
        }
    }
}