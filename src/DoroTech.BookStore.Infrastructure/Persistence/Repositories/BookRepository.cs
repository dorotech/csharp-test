namespace DoroTech.BookStore.Infrastructure.Persistence.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(BookStoreContext context) : base(context)
    {
    }
}
