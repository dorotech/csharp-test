using BookstoreManager.Domain.dto.GetAll;

namespace BookstoreManager.Application.BookService.Querie.GetAll
{
    public interface IGetAllBookService
    {
       Task<List<GetAllBookResponse>> GetAll(GetAllBookRequest request);
    }
}
