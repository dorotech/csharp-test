using DoroTechChallenge.Services.DTOs;
using DoroTechChallenge.Services.Requests;
using DoroTechChallenge.Services.Responses;
using Kumbajah.Services.Pagination;

namespace DoroTechChallenge.Services;

public interface IBookService
{
    BookDTO FetchBook(int id);
    PaginationResponse<BookDTO> PagedBooks(ListCriteria criteria);
    Task<InsertOrUpdateResponse<BookDTO>> InsertOrUpdateAsync(InsertOrUpdateBookRequest request);
    Task<DeleteResponse<BookDTO>> Remove(int id);
}
