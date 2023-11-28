

using BookstoreManager.Domain.dto.update;

namespace BookstoreManager.Application.BookService.Command.Delete
{
    public interface IRemoveBookService
    {
        Task<UpdateResponse> Remove(int id);
    }
}
