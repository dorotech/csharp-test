using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using BookststorageManager.Data.Data;

namespace BookstoreManager.Repository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;
        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Add(Book book)
        {
            await _dataContext.Books.AddAsync(book);
            await _dataContext.SaveChangesAsync();
         
        }

        public void Delete(Book book)
        {
           _dataContext.Books.Remove(book);
        }

        public  IEnumerable<Book> GetAll()
        {
           var result =  _dataContext.Books.AsEnumerable();
            return result;
        }

        public async Task<Book> GetById(int id)
        {
            var result = await _dataContext.Books.FindAsync(id);
            
            if(result == null)
                return new Book();
            return result;
        }

        public void Update(Book book)
        {
            _dataContext.Books.Update(book);
        }
        public  Book GetByName(string name)
        {
            return  _dataContext.Books.FirstOrDefault(x => x.Name.Equals(name));
           
        }
    }
}
