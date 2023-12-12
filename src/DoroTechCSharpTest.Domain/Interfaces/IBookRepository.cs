using DoroTechCSharpTest.Domain.Entities;
using DoroTechCSharpTest.Domain.Interfaces.Base;

namespace DoroTechCSharpTest.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetAsync(int id);

        Task<Book> GetByCodeAsync(string Code);

        Task<List<Book>> GetAllAsync();
    }
}
