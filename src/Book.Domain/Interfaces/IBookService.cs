using Book.Domain.Models;

namespace Book.Domain.Interfaces
{
    public interface IBookService
    {
        Task Add(BookModel book);
        Task Update(BookModel book);
        Task Remove(Guid id);
    }
}