using DTech.CityBookStore.Application.Books.Dto;

namespace DTech.CityBookStore.Application.Books;

public interface IBookService
{
    Task<BookDetailsDto> AddAsync(BookAddDto dto);
}
