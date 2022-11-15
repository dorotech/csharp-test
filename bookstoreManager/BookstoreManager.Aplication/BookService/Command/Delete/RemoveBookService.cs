using BookstoreManager.Domain.dto.update;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.BookService.Command.Delete
{
    public class RemoveBookService : IRemoveBookService
    {
        private readonly IBookRepository _bookRepository;
        public RemoveBookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<UpdateResponse> Remove(int id)
        {
            var checkHas = await _bookRepository.GetById(id);

            _bookRepository.Delete(checkHas);

            return new UpdateResponse("atualizado com sucesso !");
        }
    }
}
