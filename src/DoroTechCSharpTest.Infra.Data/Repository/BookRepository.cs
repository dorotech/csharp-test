using DoroTechCSharpTest.Domain.Entities;
using DoroTechCSharpTest.Domain.Interfaces;
using DoroTechCSharpTest.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DoroTechCSharpTest.Infra.Data.Repository
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        { }

        public async Task<List<Book>> GetAllAsync()
        {
            return await Db.Book.ToListAsync();
        }

        public async Task<Book> GetAsync(int id)
        {
            return await Db.Book.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> GetByCodeAsync(string Code)
        {
            return await Db.Book.FirstOrDefaultAsync(x => x.Code == Code);
        }
    }
}