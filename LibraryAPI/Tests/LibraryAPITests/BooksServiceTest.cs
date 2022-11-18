using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Repositories;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests
{
    public class BooksServiceTest
    {
        public Mock<IBooksRepository> booksRepositoryMock = new Mock<IBooksRepository>();
        public Mock<IUnitOfWork> unitOfWork = new Mock<IUnitOfWork>();
        public Mock<ILogger<BooksService>> logger = new Mock<ILogger<BooksService>>();

        [Fact]
        public async void SaveBookWithSuccess()
        {
            var book = new Book { Id = 1, Name = "Name", Author = "Author", ImageUrl = "SomeImageUrl" };

            var booksService = new BooksService(booksRepositoryMock.Object, unitOfWork.Object, logger.Object);
            var result = await booksService.SaveAsync(book);

            Assert.Equal(book, result.Book);
            Assert.True(result.Success);
        }

        [Fact]
        public async void SaveBookWithExistentName()
        {
            var bookToInsert = new Book { Id = 1, Name = "Name", Author = "Author", ImageUrl = "SomeImageUrl" };
            var ListBooksModel = new ListBooksModel { Page = 1, NumberOfItens = 10 };

            booksRepositoryMock
                .Setup(s => s.FindByNameAsync(bookToInsert.Name))
                .ReturnsAsync(bookToInsert);

            var usersService = new BooksService(booksRepositoryMock.Object, unitOfWork.Object, logger.Object);
            var result = await usersService.SaveAsync(bookToInsert);

            Assert.False(result.Success);
        }

        [Fact]
        public async void UpdateBookWithSuccess()
        {
            var book = new Book { Id = 1, Name = "Name", Author = "Author", ImageUrl = "SomeImageUrl" };

            booksRepositoryMock
                .Setup(s => s.FindByIdAsync(book.Id))
                .ReturnsAsync(book);

            var booksService = new BooksService(booksRepositoryMock.Object, unitOfWork.Object, logger.Object);
            var result = await booksService.UpdateAsync(1, book);

            Assert.Equal(book, result.Book);
            Assert.True(result.Success);
        }

        [Fact]
        public async void UpdateBookWithInexistentId()
        {
            var bookToUpdate = new Book { Id = 1, Name = "Name", Author = "Author", ImageUrl = "SomeImageUrl" };

            var booksService = new BooksService(booksRepositoryMock.Object, unitOfWork.Object, logger.Object);
            var result = await booksService.UpdateAsync(0, bookToUpdate);

            Assert.Null(result.Book);
            Assert.False(result.Success);
        }

        [Fact]
        public async void DeleteBookWithSuccess()
        {
            var bookToDelete = new Book { Id = 1, Name = "Name", Author = "Author", ImageUrl = "SomeImageUrl" };
            booksRepositoryMock.Setup(s => s.FindByIdAsync(1)).ReturnsAsync(bookToDelete);

            var booksService = new BooksService(booksRepositoryMock.Object, unitOfWork.Object, logger.Object);
            var result = await booksService.DeleteAsync(1);

            Assert.Equal(bookToDelete, result.Book);
            Assert.True(result.Success);
        }

        [Fact]
        public async void DeleteBookWithInexistentId()
        {
            var booksService = new BooksService(booksRepositoryMock.Object, unitOfWork.Object, logger.Object);
            var result = await booksService.DeleteAsync(0);

            Assert.Null(result.Book);
            Assert.False(result.Success);
        }
    }
}
