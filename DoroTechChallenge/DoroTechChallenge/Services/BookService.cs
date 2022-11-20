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
        var publishingCompanyCollection = request.PublishingCompanies.Select(x => new PublishingCompany(x.ToLower())).ToList();
        var result = TryDateTimeParse(request.PublishedDate, request.Id);

        bool isInsert = request.Id is 0;
        return isInsert
            ? await Insert(request, publishingCompanyCollection, result)
            : await Update(request, publishingCompanyCollection, result);
    }

    private async Task<InsertOrUpdateResponse<BookDTO>> Insert(
        InsertOrUpdateBookRequest request, List<PublishingCompany> companies, Tuple<DateTime, bool> result)
    {
        var book = new Book
        {
            Title = request.Title,
            Description = request.Description,
            PublishedDate = result.Item2 ? result.Item1 : DateTime.MaxValue,
            Author = new Author(request.Author),
            Genre = new Genre(request.Genre),
            PublishingCompanies = companies
        };
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

    private async Task<InsertOrUpdateResponse<BookDTO>> Update(
       InsertOrUpdateBookRequest request, List<PublishingCompany> companies, Tuple<DateTime, bool> result)
    {
        var existingBook = FetchBook(request.Id);
        var entityBook = BookRepository.FetchBook(request.Id);
        if (existingBook is null)
        {
            InsertOrUpdateResponse<BookDTO>.Invalid(new List<string> { "Id inválido!" });
        }
        var book = new Book
        {
            Id = request.Id,
            Title = string.IsNullOrEmpty(request.Title) ? existingBook.Title : request.Title,
            Description = string.IsNullOrEmpty(request.Description) ? existingBook.Description : request.Description,
            PublishedDate = result.Item2 ? result.Item1 : DateTime.MaxValue,
            Author = new Author(string.IsNullOrEmpty(request.Author) ? existingBook.Author : request.Author),
            Genre = new Genre(string.IsNullOrEmpty(request.Genre) ? existingBook.Genre : request.Genre),
            PublishingCompanies = !companies.Any() ? entityBook.PublishingCompanies : companies 
        };
        var validationResult = Validator.Validate(book);
        if (validationResult.IsValid)
        {
            var entity = await BookRepository.TryUpdate(book);
            var updatedDto = new BookDTO(entity.Item2);
            return InsertOrUpdateResponse<BookDTO>.Valid(validationResult, updatedDto);
        }
        return InsertOrUpdateResponse<BookDTO>.Invalid(validationResult);
    }

    private Tuple<DateTime, bool> TryDateTimeParse(string date, int bookId)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            var book = FetchBook(bookId);
            return Tuple.Create(book.PublishedDate, true);
        }
        if (IsAValidDate(date))
        {
            var day = date[0].ToString() + date[1].ToString();
            var month = date[3].ToString() + date[4].ToString();
            var year = date[6].ToString() + date[7].ToString() + date[8].ToString() + date[9].ToString();
            return Tuple.Create(new DateTime(int.Parse(year), int.Parse(month), int.Parse(day)), true);
        }
        return Tuple.Create(DateTime.MaxValue, false);
    }

    private bool IsAValidDate(string value) =>
      DateTime.TryParse(value, out DateTime date);

    public async Task<DeleteResponse<BookDTO>> DeleteAsync(int id)
    {
        var isDeleted = await BookRepository.TryDelete(id);
        return isDeleted.Item1 ?
            DeleteResponse<BookDTO>.Valid(id) :
            DeleteResponse<BookDTO>.Invalid(new List<string>() { "Erro ao deletar o livro: id não encontrado!" });
    }
}
