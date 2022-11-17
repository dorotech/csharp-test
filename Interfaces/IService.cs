using dorotec_backend_test.Classes.Pagination;

namespace dorotec_backend_test.Interfaces;

public interface IService<T> where T : class
{
    Task<T> Create(T dto);
    Task<PageResult<T>> GetPage(int index, byte size);
    Task<T> GetOne(int id);
    Task<T> UpdateOne(T dto);
    Task DeleteOne(int id);
}
