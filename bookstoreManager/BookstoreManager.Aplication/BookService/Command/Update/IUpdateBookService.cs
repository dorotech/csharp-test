using BookstoreManager.Domain.dto.update;

namespace BookstoreManager.Application.BookService.Command.Update
{
    public interface IUpdateBookService
    {
        Task<UpdateResponse> Update(UpdateRequest request);
    }
}
