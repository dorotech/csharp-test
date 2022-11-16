namespace BookManager.Repository
{
    public interface IBookRepository : IBaseRepository
    {
        Task GetBooksByIdAsync(int id);
    }
}