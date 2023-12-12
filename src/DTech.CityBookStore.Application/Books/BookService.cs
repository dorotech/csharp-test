using AutoMapper;
using DTech.CityBookStore.Application.Books.Dto;
using DTech.CityBookStore.Application.Core.Base;
using DTech.CityBookStore.Application.Core.Notifications;
using DTech.CityBookStore.Domain.Books;
using DTech.CityBookStore.Domain.Books.Repositories;
using DTech.CityBookStore.Domain.Books.Validations;
using DTech.CityBookStore.Domain.Resources.Validations;

namespace DTech.CityBookStore.Application.Books;

public class BookService : BaseService, IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper, INotifier notifier)
        : base(notifier)
    {
        _repository = repository;
        _mapper = mapper;
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
            return null;
        }

        var insertedModel = await _repository.CreateAsync(model);

        var createdDto = _mapper.Map<BookDetailsDto>(insertedModel);

        return createdDto;
    }
}
