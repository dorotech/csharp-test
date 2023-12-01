using System.Linq.Expressions;
using System.Net;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Authors.Queries.PagedList;

public class AuthorPagedListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<AuthorPagedListQuery, ReturnMessage<IPagedList<AuthorResponse>>>
{
    public async Task<ReturnMessage<IPagedList<AuthorResponse>>> Handle(AuthorPagedListQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Author, bool>> predicate = null;
        if (!string.IsNullOrEmpty(query.Search))
            predicate = author => author.Name.Contains(query.Search)
                                    || author.Biography.Contains(query.Search)
                                    || author.Id.ToString().Contains(query.Search);
        
        var authors = await unitOfWork
            .GetRepository<Author>()
            .GetPagedListAsync(
                selector: author => mapper.Map<AuthorResponse>(author),
                predicate: predicate,
                orderBy: report => report.OrderByDescending(r => r.CreatedAt),
                pageIndex: query.PageIndex,
                pageSize: query.PageSize,
                cancellationToken: cancellationToken);


        if (authors.TotalCount == 0)
            return new ReturnMessage<IPagedList<AuthorResponse>>(data: null, HttpStatusCode.NoContent);

        return new ReturnMessage<IPagedList<AuthorResponse>>(authors, HttpStatusCode.OK);
    }
}