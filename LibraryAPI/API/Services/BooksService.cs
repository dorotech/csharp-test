using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Repositories;
using LibraryApi.Domain.Services;
using LibraryApi.Domain.Services.Communication;
using LibraryApi.Models;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BooksService> _logger;

        public BooksService(IBooksRepository booksRepository, IUnitOfWork unitOfWork, ILogger<BooksService> logger)
        {
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> ListAsync(ListBooksModel model)
        {
            return await _booksRepository.ListAsync(model);
        }

        public async Task<BookResponse> UpdateAsync(int id, Book book)
        {
            var existingBook = await _booksRepository.FindByIdAsync(id);
            if (existingBook == null)
                return new BookResponse("Book not found.");

            var bookWithSameName = await _booksRepository.FindByNameAsync(book.Name);
            if (bookWithSameName != null)
                return new BookResponse($"A book with the same name already exists.");

            existingBook.Name = book.Name;
            existingBook.Author = book.Author;
            existingBook.ImageUrl = book.ImageUrl;

            try
            {
                _booksRepository.Update(existingBook);
                await _unitOfWork.CompleteAsync();

                _logger.Log(LogLevel.Information, $"Book with id {existingBook.Id} updated with Success");
                return new BookResponse(existingBook);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An error occurred when trying to update the Book with id {id}: {ex.Message}");
                return new BookResponse($"An error occurred when updating the Book: {ex.Message}");
            }
        }

        public async Task<BookResponse> SaveAsync(Book book)
        {
            try
            {
                var bookWithSameName = await _booksRepository.FindByNameAsync(book.Name);
                if (bookWithSameName != null)
                    return new BookResponse($"A book with the same name already exists.");

                await _booksRepository.AddAsync(book);
                await _unitOfWork.CompleteAsync();

                _logger.Log(LogLevel.Information, $"Book with id {book.Id} saved with Success");
                return new BookResponse(book);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An error occurred when trying to save a Book: {ex.Message}");
                return new BookResponse($"An error occurred when saving the Book: {ex.Message}");
            }
        }

        public async Task<BookResponse> DeleteAsync(int id)
        {
            var existingBook = await _booksRepository.FindByIdAsync(id);

            if (existingBook == null)
                return new BookResponse("Book not found.");

            try
            {
                _booksRepository.Remove(existingBook);
                await _unitOfWork.CompleteAsync();

                _logger.Log(LogLevel.Information, $"Book with id {id} deleted with Success");
                return new BookResponse(existingBook);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An error occurred when trying to delete the Book with id {id}: {ex.Message}");
                return new BookResponse($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}