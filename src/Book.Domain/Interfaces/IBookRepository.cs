using Book.Domain.Models;

namespace Book.Domain.Interfaces
{
    public interface IBookRepository : IRepository<BookModel>
    {
        Task<BookModel> GetBookByName(string name);
    }
}