using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Repositories;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests
{
    public class UsersServiceTest
    {
        public Mock<IUsersRepository> usersRepositoryMock = new Mock<IUsersRepository>();
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        public IConfiguration configurationForUnitTests;

        public UsersServiceTest()
        {
            var configuration = new Dictionary<string, string>
            {
                {"Authentication:SecretKey", "fedaf7d8863b48e197b9287d492b708e"},
            };

            configurationForUnitTests = new ConfigurationBuilder()
                .AddInMemoryCollection(configuration)
                .Build();
        }

        [Fact]
        public async void SaveUserWithSuccess()
        {
            var user = new User { Email = "email@email.com", Password = "password", Role = UserRoleEnum.BASICO };

            var usersService = new UsersService(usersRepositoryMock.Object, unitOfWork.Object, configurationForUnitTests);
            var result = await usersService.SaveAsync(user, user.Password);

            Assert.Equal(user, result.User);
        }

        [Fact]
        public async void SaveUserWithExistentEmail()
        {
            var user = new User { Email = "email@email.com", Password = "password", Role = UserRoleEnum.BASICO };

            usersRepositoryMock
                .Setup(s => s.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var usersService = new UsersService(usersRepositoryMock.Object, unitOfWork.Object, configurationForUnitTests);
            var result = await usersService.SaveAsync(user, user.Password);

            Assert.False(result.Success);
        }

        [Fact]
        public async void LoginWithSuccess()
        {
            var user = new User
            {
                Email = "email@email.com",
                Password = "$2b$10$epQGPBtEbYioEDCg7x/jjuggbcIGCNuoEj4UIUTf/fdeC7rXeJJQq",
                Role = UserRoleEnum.BASICO
            };
            var loginUserResource = new LoginUserModel { Email = "email@email.com", Password = "123456" };

            usersRepositoryMock
                .Setup(s => s.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var usersService = new UsersService(usersRepositoryMock.Object, unitOfWork.Object, configurationForUnitTests);
            var result = await usersService.LoginAsync(loginUserResource);

            Assert.True(result.Success);
        }

        [Fact]
        public async void LoginWithWrongPassword()
        {
            var user = new User
            {
                Email = "email@email.com",
                Password = "$2b$10$epQGPBtEbYioEDCg7x/jjuggbcIGCNuoEj4UIUTf/fdeC7rXeJJQq",
                Role = UserRoleEnum.BASICO
            };
            var loginUserResource = new LoginUserModel { Email = "email@email.com", Password = "123" };

            usersRepositoryMock
                .Setup(s => s.GetUserByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var usersService = new UsersService(usersRepositoryMock.Object, unitOfWork.Object, configurationForUnitTests);
            var result = await usersService.LoginAsync(loginUserResource);

            Assert.False(result.Success);
        }
    }
}