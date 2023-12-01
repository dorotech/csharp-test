using System.Linq.Expressions;
using System.Net;
using Application.Common.Collections;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Categories.Queries.PagedList;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Categories.Handlers;

[Trait(nameof(CategoryPagedListQuery), "Handler")]
public class CategoryPagedListQueryHandlerTests : TestBase
{
    private readonly CategoryPagedListQueryHandler _handler;
    private readonly IRepository<Domain.Entities.Category> _categoryRepository;

    public CategoryPagedListQueryHandlerTests()
    {
        _handler = new CategoryPagedListQueryHandler(_mapper, _unitOfWork);
        _categoryRepository = A.Fake<IRepository<Domain.Entities.Category>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Category>()).Returns(_categoryRepository);
    }

    [Fact]
    public async Task CategoryPagedListQueryHandler_NoContent_ReturnSuccessAsync()
    {
        var query = new CategoryPagedListQuery();
        IPagedList<Domain.Entities.Category> categoryPagedList = new PagedList<Domain.Entities.Category>
        {
            TotalCount = 0
        };

        A.CallTo(_categoryRepository.GetPagedListAsyncFunc()).Returns(categoryPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task CategoryPagedListQueryHandler_WithContent_ReturnSuccessAsync()
    {
        var query = new CategoryPagedListQuery();
        IPagedList<CategoryResponse> categoryPagedList = new PagedList<CategoryResponse>
        {
            TotalCount = 2,
            Items = new List<CategoryResponse>
            {
                new CategoryResponse
                {
                    Id = Guid.NewGuid(),
                    Name = _faker.Person.FullName,
                }
            }
        };

        A.CallTo(() => _categoryRepository.GetPagedListAsync(
            A<Expression<Func<Domain.Entities.Category, CategoryResponse>>>.Ignored,
            A<Expression<Func<Domain.Entities.Category, bool>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.Category>, IOrderedQueryable<Domain.Entities.Category>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.Category>, IIncludableQueryable<Domain.Entities.Category, object>>>.Ignored,
            A<int>.Ignored,
            A<int>.Ignored,
            A<bool>.Ignored,
            false,
            A<CancellationToken>.Ignored
        )).Returns(categoryPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}