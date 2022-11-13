using APIBook.Model;
using Microsoft.EntityFrameworkCore;

namespace APIBook.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> Get();
        public Task<IEnumerable<Book>> GetByName(string title);
        public Task<Book> GetById(int id);
        public Task<Book> Create(Book book);
        public Task Update(Book book);
        public Task Delete(int id);
    }

}
