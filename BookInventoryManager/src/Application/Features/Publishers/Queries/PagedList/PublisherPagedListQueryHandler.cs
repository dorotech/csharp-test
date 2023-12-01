using System.Linq.Expressions;
using System.Net;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Authors.Queries.PagedList;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Publishers.Queries.PagedList;

public class PublisherPagedListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<PublisherPagedListQuery, ReturnMessage<IPagedList<PublisherResponse>>>
{
    public async Task<ReturnMessage<IPagedList<PublisherResponse>>> Handle(PublisherPagedListQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Publisher, bool>> predicate = null;
        if (!string.IsNullOrEmpty(query.Search))
            predicate = category => category.Name.Contains(query.Search)
                                    || category.Id.ToString().Contains(query.Search);
        
        var publishers = await unitOfWork
            .GetRepository<Publisher>()
            .GetPagedListAsync(
                selector: category => mapper.Map<PublisherResponse>(category),
                predicate: predicate,
                orderBy: report => report.OrderByDescending(r => r.CreatedAt),
                pageIndex: query.PageIndex,
                pageSize: query.PageSize,
                cancellationToken: cancellationToken);


        if (publishers.TotalCount == 0)
            return new ReturnMessage<IPagedList<PublisherResponse>>(data: null, HttpStatusCode.NoContent);

        return new ReturnMessage<IPagedList<PublisherResponse>>(publishers, HttpStatusCode.OK);
    }
}