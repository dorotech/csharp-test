using BookstoreManager.Domain.dto.GetAll;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.BookService.Querie.GetAll
{
    public class GetAllBookService : IGetAllBookService
    {

        private readonly IBookRepository _bookRepository;
        public GetAllBookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;

        }
        public async Task<List<GetAllBookResponse>> GetAll(GetAllBookRequest request)
        {
            return await Task.Run(() =>
            {
                var books = _bookRepository.GetAll()
                                           .Where(e => ((!string.IsNullOrEmpty(request.Search) ||
                                                        e.Name.Contains(request.Search) ||
                                                        e.Author.Contains(request.Search) ||
                                                        e.Description.Contains(request.Search)) &&
                                                        e.Active))
                                           .Select(res => new GetAllBookResponse
                                           {
                                               Id = res.Id,
                                               Name = res.Name,
                                               Description = res.Description,
                                               Genre = res.Genre,
                                               Author = res.Author,
                                               CreateAt = res.CreateAt,
                                               UpdateAt = res.UpdateAt
                                           }).Skip((request.Page - 1) * request.PageSize)
                                             .Take(request.PageSize).ToList();
                books.OrderBy(n => n.Name);

                return books;
            });             
        }
    }
}
