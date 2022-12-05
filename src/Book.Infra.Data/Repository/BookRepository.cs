using Book.Domain.Interfaces;
using Book.Domain.Models;
using Book.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Book.Infra.Data.Repository
{
    public class BookRepository : Repository<BookModel>, IBookRepository
    {
        public BookRepository(BookApiDbContext context) : base(context) { }

        public async Task<BookModel> GetBookByName(string name)
        {
            return await Db.Books.AsNoTracking()
                .Include(c => c.Name)
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}