using System.Linq.Expressions;
using System.Net;
using Application.Common.Collections;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Publishers.Queries.PagedList;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;
using Tests.Extensions;
using Xunit;

namespace Tests.Features.Publishers.Handlers;

[Trait(nameof(PublisherPagedListQuery), "Handler")]
public class PublisherPagedListQueryHandlerTests : TestBase
{
    private readonly PublisherPagedListQueryHandler _handler;
    private readonly IRepository<Domain.Entities.Publisher> _publisherRepository;

    public PublisherPagedListQueryHandlerTests()
    {
        _handler = new PublisherPagedListQueryHandler(_mapper, _unitOfWork);
        _publisherRepository = A.Fake<IRepository<Domain.Entities.Publisher>>();
        A.CallTo(() => _unitOfWork.GetRepository<Domain.Entities.Publisher>()).Returns(_publisherRepository);
    }

    [Fact]
    public async Task PublisherPagedListQuery_NoContent_ReturnSuccessAsync()
    {
        var query = new PublisherPagedListQuery();
        IPagedList<Domain.Entities.Publisher> publisherPagedList = new PagedList<Domain.Entities.Publisher>
        {
            TotalCount = 0
        };

        A.CallTo(_publisherRepository.GetPagedListAsyncFunc()).Returns(publisherPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task PublisherPagedListQuery_WithContent_ReturnSuccessAsync()
    {
        var query = new PublisherPagedListQuery();
        IPagedList<PublisherResponse> publisherPagedList = new PagedList<PublisherResponse>
        {
            TotalCount = 2,
            Items = new List<PublisherResponse>
            {
                new PublisherResponse
                {
                    Id = Guid.NewGuid(),
                    Name = _faker.Person.FullName,
                }
            }
        };
        
        A.CallTo(() => _publisherRepository.GetPagedListAsync(
            A<Expression<Func<Domain.Entities.Publisher, PublisherResponse>>>.Ignored,
            A<Expression<Func<Domain.Entities.Publisher, bool>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.Publisher>, IOrderedQueryable<Domain.Entities.Publisher>>>.Ignored,
            A<Func<IQueryable<Domain.Entities.Publisher>, IIncludableQueryable<Domain.Entities.Publisher, object>>>.Ignored,
            A<int>.Ignored,
            A<int>.Ignored,
            A<bool>.Ignored,
            false,
            A<CancellationToken>.Ignored
        )).Returns(publisherPagedList);

        var result = await _handler.Handle(query, default);

        Assert.True(result.IsSuccess);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }
}