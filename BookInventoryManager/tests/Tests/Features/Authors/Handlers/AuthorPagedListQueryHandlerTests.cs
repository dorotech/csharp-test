using System.Linq.Expressions;
using System.Net;
using Application.Common.Collections;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Authors.Queries.PagedList;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Authors.Handlers;

[Trait(nameof(AuthorPagedListQuery), "Handler")]
public class AuthorPagedListQueryHandlerTests : TestBase
{
    private readonly AuthorPagedListQueryHandler _handler;
    private readonly IRepository<Domain.Entities.Author> _authorRepository;

    public AuthorPagedListQueryHandlerTests()
    {
        _handler = new AuthorPagedListQueryHandler(_mapper, _unitOfWork);
        _authorRepository = A.Fake<IRepository<Domain.Entities.Author>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Author>()).Returns(_authorRepository);
    }

    [Fact]
    public async Task AuthorPagedListQuery_NoContent_ReturnSuccessAsync()
    {
        var query = new AuthorPagedListQuery();
        IPagedList<Domain.Entities.Author> authorPagedList = new PagedList<Domain.Entities.Author>
        {
            TotalCount = 0
        };

        A.CallTo(_authorRepository.GetPagedListAsyncFunc()).Returns(authorPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task AuthorPagedListQuery_WithContent_ReturnSuccessAsync()
    {
        var query = new AuthorPagedListQuery();
        IPagedList<AuthorResponse> authorPagedList = new PagedList<AuthorResponse>
        {
            TotalCount = 2,
            Items = new List<AuthorResponse>
            {
                new AuthorResponse
                {
                    Id = Guid.NewGuid(),
                    Name = _faker.Person.FullName,
                    Biography = _faker.Lorem.Sentence()
                }
            }
        };
        
        A.CallTo(() => _authorRepository.GetPagedListAsync(
            A<Expression<Func<Domain.Entities.Author, AuthorResponse>>>.Ignored,
            A<Expression<Func<Domain.Entities.Author, bool>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.Author>, IOrderedQueryable<Domain.Entities.Author>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.Author>, IIncludableQueryable<Domain.Entities.Author, object>>>.Ignored,
            A<int>.Ignored,
            A<int>.Ignored,
            A<bool>.Ignored,
            false,
            A<CancellationToken>.Ignored
        )).Returns(authorPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}