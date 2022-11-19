using dorotec_backend_test.Classes.DTOs;
using dorotec_backend_test.Classes.Pagination;

namespace dorotec_backend_test.Interfaces;

public interface IBookService : IService<BookDTO>
{ 
    Task<PageResult<BookDTO>> GetPage(int index, byte size, BookFilterDTO filter);
}
