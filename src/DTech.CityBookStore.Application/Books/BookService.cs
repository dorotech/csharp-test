using AutoMapper;
using DTech.CityBookStore.Application.Books.Dto;
using DTech.CityBookStore.Application.Core.Base;
using DTech.CityBookStore.Application.Core.Notifications;
using DTech.CityBookStore.Domain.Books;
using DTech.CityBookStore.Domain.Books.Repositories;
using DTech.CityBookStore.Domain.Books.Validations;
using DTech.CityBookStore.Domain.Resources.Validations;
using DTech.Domain.Core.Pagination;
using Microsoft.Extensions.Logging;

namespace DTech.CityBookStore.Application.Books;

public class BookService : BaseService, IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository repository, 
                       IMapper mapper,
                       INotifier notifier,
                       ILogger<BookService> logger)
        : base(notifier)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookDetailsDto> GetAsync(int id)
    {
        var model = await _repository.GetAsync(id);
        var dto = _mapper.Map<BookDetailsDto>(model);
        return dto;
    }

    public async Task<BookDetailsDto> AddAsync(BookAddDto dto)
    {
        var model = _mapper.Map<Book>(dto);

        var validator = new BookValidator();

        if (!Validate(validator, model))
        {
            return null;
        }

        var hasBookWithISBNS = await _repository.ExistsAsync(model.ISBN10, model.ISBN13);

        if (hasBookWithISBNS)
        {
            Notify(BookMessageValidationResources.ExistsBook);
            _logger.LogError(BookMessageValidationResources.ExistsBook);
            return null;
        }

        var insertedModel = await _repository.CreateAsync(model);

        var createdDto = _mapper.Map<BookDetailsDto>(insertedModel);
        _logger.LogInformation($"Book created: {model.ToString()}");

        return createdDto;
    }

    public async Task<BookDetailsDto> UpdateAsync(int id, BookUpdateDto dto)
    {
        var model = _mapper.Map<Book>(dto);

        if (model.Id != id)
        {
            Notify(BookMessageValidationResources.BookIdDifferent);
            return null;
        }

        var validator = new BookValidator();

        if (!Validate(validator, model))
        {
            return null;
        }

        var hasBookWithId = await _repository.ExistsAsync(id);

        if (!hasBookWithId)
        {
            Notify(string.Format(BookMessageValidationResources.BookDontExists, id));
            _logger.LogError(BookMessageValidationResources.BookDontExists, id);
            return null;
        }

        var hasBookWithISBNSAndDifferentId = await _repository.ExistsAsync(model.Id, model.ISBN10, model.ISBN13);

        if (hasBookWithISBNSAndDifferentId)
        {
            Notify(BookMessageValidationResources.ExistsBook);
            _logger.LogError($"{BookMessageValidationResources.ExistsBook} ISBN10: {model.ISBN10} and ISBN13: {model.ISBN13}.");
            return null;
        }

        var insertedModel = await _repository.UpdateAsync(model);

        var updatedDto = _mapper.Map<BookDetailsDto>(insertedModel);
        _logger.LogInformation($"Book updated: {model.ToString()}");

        return updatedDto;
    }

    public async Task DeleteAsync(int id) 
    {
        var hasBookWithId = await _repository.ExistsAsync(id);

        if (!hasBookWithId)
        {
            Notify(string.Format(BookMessageValidationResources.BookDontExists, id));
            _logger.LogError(BookMessageValidationResources.BookDontExists, id);
            return;
        }
                
        await _repository.DeleteAsync(id);
        _logger.LogInformation($"Book deleted Id: {id}.");
    }

    public async Task<PagedResult<BookDetailsDto>> FindByFiltersAsync(int? id,
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
        int pageSize)
    {
        var models = await _repository.FindByFiltersAsync(id,
            title, 
            author,
            language,
            minEdition,
            maxEdition,
            minPages,
            maxPages,
            publishing,
            isbn10,
            isbn13,
            page,
            pageSize);

        var dtos = models.CopyToDtoMapping(_mapper.Map<List<BookDetailsDto>>(models.Items));

        return dtos;
    }
}
