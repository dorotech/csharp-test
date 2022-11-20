using DoroTechChallenge.Models;
using DoroTechChallenge.Repositories;
using DoroTechChallenge.Services.DTOs;
using DoroTechChallenge.Services.Requests;
using DoroTechChallenge.Services.Responses;
using FluentValidation;
using Kumbajah.Services.Pagination;

namespace DoroTechChallenge.Services;

public class BookService : IBookService
{
    private IBookRepository BookRepository { get; }
    private IValidator<Book> Validator { get; }

    public BookService(
        IBookRepository bookRepository,
        IValidator<Book> validator)
    {
        BookRepository = bookRepository;
        Validator = validator;
    }

    public BookDTO FetchBook(int id)
    {
        var entity = BookRepository.FetchBook(id);
        return new BookDTO(entity);
    }

    public PaginationResponse<BookDTO> PagedBooks(ListCriteria criteria)
    {
        if (criteria is null) throw new ArgumentNullException(nameof(criteria));

        var filteredBooks = BookRepository.Filter(criteria.Filter);
        var paginatedBooks = filteredBooks.Paginate(criteria.Pagination);
        var booksDTO = paginatedBooks.Select(b => new BookDTO(b));
        return new PaginationResponse<BookDTO>(filteredBooks.Count(), booksDTO.ToList());
    }

    public async Task<InsertOrUpdateResponse<BookDTO>> InsertOrUpdateAsync(InsertOrUpdateBookRequest request)
    {
        var publishingCompanyCollection = request.PublishingCompanies.Select(x => new PublishingCompany(x)).ToList();
        var book = new Book
        {
            Title = request.Title,
            Description = request.Description,
            PublishedDate = ToDateTime(request.PublishedDate),
            Author = new Author(request.Author),
            Genre = new Genre(request.Genre),
            PublishingCompanies = publishingCompanyCollection
        };
        bool isInsert = request.Id is 0;
        return isInsert
            ? await Insert(book)
            : await Update(book);
    }

    private async Task<InsertOrUpdateResponse<BookDTO>> Update(Book book)
    {
        var validationResult = Validator.Validate(book);
        if (validationResult.IsValid)
        {
            var entity = await BookRepository.UpdateAsync(book);
            var updatedDto = new BookDTO(entity);
            return InsertOrUpdateResponse<BookDTO>.Valid(validationResult, updatedDto);
        }
        return InsertOrUpdateResponse<BookDTO>.Invalid(validationResult);
    }

    private async Task<InsertOrUpdateResponse<BookDTO>> Insert(Book book)
    {
        var validationResult = Validator.Validate(book, o => o.IncludeRuleSets("Insert"));
        if (validationResult.IsValid)
        {
            var proxy = await BookRepository.InsertAsync(book);
            var dto = new BookDTO(proxy);
            return InsertOrUpdateResponse<BookDTO>.Valid(validationResult, dto);
        }
        else
        {
            return InsertOrUpdateResponse<BookDTO>.Invalid(validationResult);
        }
    }

    private DateTime ToDateTime(string date)
    {
        if (IsAValidDate(date))
        {
            var day = date[0] + date[1];
            var month = date[3] + date[4];
            var year = date[6] + date[7] + date[8] + date[9];
            return new DateTime(year, month, day);
        }
        return DateTime.MinValue;
    }

    private bool IsAValidDate(string value) =>
      DateTime.TryParse(value, out DateTime date);

    public async Task<DeleteResponse<BookDTO>> Remove(int id) =>
        await BookRepository.TryDelete(id) ?
            DeleteResponse<BookDTO>.Valid(id) :
            DeleteResponse<BookDTO>.Invalid(new List<string>() { "Erro ao deletar o livro!" });
}
