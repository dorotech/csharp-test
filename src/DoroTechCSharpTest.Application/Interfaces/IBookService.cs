using DoroTechCSharpTest.Application.ViewModel;

namespace DoroTechCSharpTest.Application.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<BookViewModel> GetAsync(int id);

        Task<List<BookViewModel>> GetAllAsync();

        Task<bool> RegisterAsync(BookViewModel model);

        Task<bool> UpdateAsync(BookViewModel model);

        Task<bool> RemoveAsync(int id);
    }
}
