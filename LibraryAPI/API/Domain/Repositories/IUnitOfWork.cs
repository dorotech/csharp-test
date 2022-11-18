namespace LibraryApi.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}