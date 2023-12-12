using AutoMapper;
using DoroTechCSharpTest.Application.Interfaces;
using DoroTechCSharpTest.Application.Services.Base;
using DoroTechCSharpTest.Application.ViewModel;
using DoroTechCSharpTest.Domain.Entities;
using DoroTechCSharpTest.Domain.Entities.Validations.BookValidator;
using DoroTechCSharpTest.Domain.Interfaces;
using DoroTechCSharpTest.Domain.Notifications;
using System.Reflection;

namespace DoroTechCSharpTest.Application.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookService(IMapper mapper, IBookRepository bookRepository, INotifier notifier) : base(notifier)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<List<BookViewModel>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            return _mapper.Map<List<BookViewModel>>(books);
        }

        public async Task<BookViewModel> GetAsync(int id)
        {
            var book = await _bookRepository.GetAsync(id);

            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<bool> RegisterAsync(BookViewModel model)
        {
            if(model == null)
            {
                Notify("Please, provide the book model");
                return false;
            }

            model.Id = null;

            var book = _mapper.Map<Book>(model);

            if (!RunValidation(new BookRegisterValidation(), book)) return false;

            if (await BookExists(model.Code))
            {
                Notify("The Code provided already exists");
                return false;
            }

            await _bookRepository.Add(book);

            return true;
        }

        public async Task<bool> UpdateAsync(BookViewModel model)
        {
            if (model == null)
            {
                Notify("Please, provide the book model");
                return false;
            }

            var book = _mapper.Map<Book>(model);

            if (!RunValidation(new BookUpdateValidation(), book)) return false;

            if (!await BookExists((int)model.Id!))
            {
                Notify("The Code provided does not exists");
                return false;
            }

            await _bookRepository.Update(book);

            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            if (!RunValidation(new BookRemoveValidation(), new Book() { Id = id })) return false;

            if (!await BookExists(id))
            {
                Notify("The Book ID provided does not exists");
                return false;
            }

            await _bookRepository.Delete(id);
            
            return true;
        }

        private async Task<bool> BookExists(string Code)
        {
            return ((await _bookRepository.GetByCodeAsync(Code))?.Code != null);
        }

        private async Task<bool> BookExists(int id)
        {
            return ((await _bookRepository.Get(id))?.Code != null);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
