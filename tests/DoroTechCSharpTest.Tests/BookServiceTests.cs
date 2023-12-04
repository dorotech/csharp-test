using AutoMapper;
using DoroTechCSharpTest.Application.AutoMapper;
using DoroTechCSharpTest.Application.Services;
using DoroTechCSharpTest.Application.ViewModel;
using DoroTechCSharpTest.Domain.Interfaces;
using DoroTechCSharpTest.Domain.Notifications;
using DoroTechCSharpTest.Infra.Data.Context;
using DoroTechCSharpTest.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DoroTechCSharpTest.Tests
{
    [TestClass]
    public class BookServiceTests
    {
        private IMapper _mapper;
        private IBookRepository _bookRepository;
        private INotifier _notifier;

        [TestInitialize]
        public void Initialize()
        {
            // Configure AutoMapper, mock IBookRepository, and INotifier
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = new Mapper(configuration);

            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryBookDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            var dbContext = new AppDbContext(options);
            _bookRepository = new BookRepository(dbContext);

            _notifier = new Mock<INotifier>().Object;
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnListOfBookViewModels()
        {
            // Arrange
            var service = new BookService(_mapper, _bookRepository, _notifier);
            var newBookModel = new BookViewModel { Title = "Teste", Author = "Teste", Code = "123-123-1", Description = "", Rating = 1, ReleaseYear = 2012 };

            // Act
            await service.RegisterAsync(newBookModel);
            var result = await service.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);

        }

        [TestMethod]
        public async Task GetAsync_NonExistingId_ShouldReturnNull()
        {
            // Arrange
            var service = new BookService(_mapper, _bookRepository, _notifier);
            var nonExistingId = 999; // Provide a non-existing book ID

            // Act
            var result = await service.GetAsync(nonExistingId);

            // Assert
            Assert.IsNull(result);

        }

        [TestMethod]
        public async Task RegisterAsync_NewBook_ShouldReturnTrue()
        {
            // Arrange
            var service = new BookService(_mapper, _bookRepository, _notifier);
            var newBookModel = new BookViewModel { Title = "Teste", Author = "Teste", Code = "123-123-2", Description = "", Rating = 1, ReleaseYear = 2012 };

            // Act
            var result = await service.RegisterAsync(newBookModel);

            // Assert
            Assert.IsTrue(result);

        }


        [TestMethod]
        public async Task UpdateAsync_NonExistingBook_ShouldReturnFalse()
        {
            // Arrange
            var service = new BookService(_mapper, _bookRepository, _notifier);
            var nonExistingBookModel = new BookViewModel { Id = 999, Title = "Teste", Author = "Teste", Code = "123-123-4", Description = "", Rating = 1, ReleaseYear = 2012 };

            // Act
            var result = await service.UpdateAsync(nonExistingBookModel);

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public async Task RemoveAsync_ExistingId_ShouldReturnTrue()
        {
            // Arrange
            var service = new BookService(_mapper, _bookRepository, _notifier);
            var existingId = 1; // Provide an existing book ID

            // Act
            var result = await service.RemoveAsync(existingId);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public async Task RemoveAsync_NonExistingId_ShouldReturnFalse()
        {
            // Arrange
            var service = new BookService(_mapper, _bookRepository, _notifier);
            var nonExistingId = 999; // Provide a non-existing book ID

            // Act
            var result = await service.RemoveAsync(nonExistingId);

            // Assert
            Assert.IsFalse(result);

        }
    }
}