using LibraryApi.Domain.Entities;
using LibraryApi.Domain.Repositories;
using LibraryApi.Persistence.Contexts;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Persistence.Repositories
{
    public class BooksRepository : BaseRepository, IBooksRepository
    {
        public BooksRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> ListAsync(ListBooksModel model)
        {
            var lastId = (model.Page - 1) * model.NumberOfItens;
            var numberOfItens = model.NumberOfItens != 0 ? model.NumberOfItens : int.MaxValue;

            return await _context.Books
                        .OrderBy(d => d.Name)
                        .Where(d => d.Id > lastId)
                        .Take(numberOfItens)
                        .ToListAsync();
        }

        public async Task AddAsync(Book Book)
        {
            await _context.Books.AddAsync(Book);
        }

        public async Task<Book?> FindByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public void Update(Book Book)
        {
            _context.Books.Update(Book);
        }

        public void Remove(Book category)
        {
            _context.Books.Remove(category);
        }

        public async Task<Book?> FindByNameAsync(string name)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Name == name);
        }
    }
}