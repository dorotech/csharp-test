using api.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories.book
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Get(int pageNumber, int pageSize, string orderBy, string search);
        Task<Book> Get(int Id);
        Task<Book> Get(string author, string title);
        Task<Book> Post(Book book);
        Task Put(Book book);
        Task Delete(int Id);
    }
}
