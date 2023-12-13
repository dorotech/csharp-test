using DTech.CityBookStore.Application.Books.Dto;
using DTech.Domain.Core.Pagination;

namespace DTech.CityBookStore.Application.Books;

public interface IBookService
{
    Task<BookDetailsDto> GetAsync(int id);
    Task<BookDetailsDto> AddAsync(BookAddDto dto);
    Task<BookDetailsDto> UpdateAsync(int id, BookUpdateDto dto);
    Task DeleteAsync(int id);
    Task<PagedResult<BookDetailsDto>> FindByFiltersAsync(int? id,
        string title,
        string author,
        string language,
        int? minEdition,
        int? maxEdition,
        int? minPages,
        int? maxPages,
        string publishing,
        string isbn10,
        string isbn13,
        int page,
        int pageSize);
}
