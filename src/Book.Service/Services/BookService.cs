using Book.Domain.Interfaces;
using Book.Domain.Models;
using Book.Domain.Models.Validations;

namespace Book.Service.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository, INotifier notifier) : base(notifier)
        {
            _bookRepository = bookRepository;
        }

        public async Task Add(BookModel book)
        {
            if (!ExecuteValidation(new BookValidation(), book)) return;

            await _bookRepository.Add(book);
        }

        public async Task Remove(Guid id)
        {
            await _bookRepository.Remove(id);
        }

        public async Task Update(BookModel book)
        {
            if (!ExecuteValidation(new BookValidation(), book)) return;

            await _bookRepository.Update(book);
        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}