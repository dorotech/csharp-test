using APIBook.Model;
using Microsoft.EntityFrameworkCore;

namespace APIBook.Repository
{
    public class BookRepository : IBookRepository
    {
        public readonly BookContext _ctx;
        public BookRepository(BookContext context)
        {
            _ctx = context;
        }

        //Metodos asyncronos
        async Task<Book> IBookRepository.Create(Book book)
        {
            if (_ctx.Books == null)
                return book;

            await _ctx.Books.AddAsync(book);
            await _ctx.SaveChangesAsync();
            return book;
        }

        async Task IBookRepository.Delete(int id)
        {

            Book? book_for_delete = await _ctx.Books.FindAsync(id);
            if (book_for_delete != null)
            {
                _ = _ctx.Remove(book_for_delete);
                await _ctx.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Book>> IBookRepository.Get()
        {
            return await _ctx.Books.ToListAsync();
        }

        async Task<IEnumerable<Book>> IBookRepository.GetByName(string title)
        {
            var books = await _ctx.Books.ToListAsync();
            List<Book> books_ = new List<Book>();
            foreach (var b in books)
            {
                if (b.Title == null)
                    continue;

                if (b.Title.Contains(title))
                    books_.Add(b);
            }
        
            return books_;
        }

        async Task<Book?> IBookRepository.GetById(int id)
        {
            return await _ctx.Books.FindAsync(id);
        }

        async Task IBookRepository.Update(Book book)
        {
            _ctx.Entry(book).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}
