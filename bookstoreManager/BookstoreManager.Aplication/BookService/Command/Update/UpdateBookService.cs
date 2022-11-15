using BookstoreManager.Domain.dto.update;
using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.BookService.Command.Update
{
    public class UpdateBookService : IUpdateBookService
    {
        private readonly IBookRepository _bookRepository;
        public UpdateBookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<UpdateResponse> Update(UpdateRequest request)
        {

            var checkHasBook = await _bookRepository.GetById(request.Id);

            var book = new Book
            {
                Name = request.Name ?? checkHasBook.Name,
                Description = request.Description ?? checkHasBook.Description,
                Author = request.Author ?? checkHasBook.Author,
                CreateAt = checkHasBook.CreateAt,
                UpdateAt = DateTime.Now,
                Active = request.Active == null ? true : false

            };

            await _bookRepository.Add(book);

            return new  UpdateResponse("atualizado com sucesso");
        }
    }
}
