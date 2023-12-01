using System.Linq.Expressions;
using System.Net;
using Application.Common.Collections;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Books.Queries.PagedList;
using Domain.Entities;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Books.Handlers;

[Trait(nameof(BookPagedListQuery), "Handler")]
public class BookPagedListQueryHandlerTests : TestBase
{
    private readonly BookPagedListQueryHandler _handler;
    private readonly IRepository<Book> _bookRepository;

    public BookPagedListQueryHandlerTests()
    {
        _handler = new BookPagedListQueryHandler(_mapper, _unitOfWork);
        _bookRepository = A.Fake<IRepository<Book>>();
        A.CallTo(() => _unitOfWork.GetRepository<Book>()).Returns(_bookRepository);
    }

    [Fact]
    public async Task BookPagedListQuery_NoContent_ReturnSuccessAsync()
    {
        var query = new BookPagedListQuery();
        IPagedList<Book> bookPagedList = new PagedList<Book>
        {
            TotalCount = 0
        };

        A.CallTo(_bookRepository.GetPagedListAsyncFunc()).Returns(bookPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task BookPagedListQuery_WithContent_ReturnSuccessAsync()
    {
        var query = new BookPagedListQuery();
        IPagedList<BookResponse> bookPagedList = new PagedList<BookResponse>
        {
            TotalCount = 2,
            Items = new List<BookResponse>
            {
                new BookResponse
                {
                    Id = Guid.NewGuid(),
                    Author = new AuthorResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = _faker.Person.FullName,
                        Biography = _faker.Lorem.Sentence()
                    },
                    Active = true,
                    Category = new CategoryResponse
                    {
                        Id = Guid.NewGuid(),
                        Name = _faker.Commerce.Categories(1)[0],
                        Description = _faker.Lorem.Sentence()
                    },
                    Description = _faker.Lorem.Sentence(),
                    Edition = _faker.Random.String(1, 2),
                    Isbn = _faker.Random.Int(10, 1000),
                    ImageUrl = _faker.Image.PicsumUrl(),
                }
            }
        };

        A.CallTo(() => _bookRepository.GetPagedListAsync(
            A<Expression<Func<Book, BookResponse>>>.Ignored,
            A<Expression<Func<Book, bool>>>.Ignored,
            A<Func<IQueryable<Book>, IOrderedQueryable<Book>>>.Ignored,
            A<Func<IQueryable<Book>, IIncludableQueryable<Book, object>>>.Ignored,
            A<int>.Ignored,
            A<int>.Ignored,
            A<bool>.Ignored,
            false,
            A<CancellationToken>.Ignored
        )).Returns(bookPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}