using System.Linq.Expressions;
using System.Net;
using Application.Common.Responses;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Requests;
using AutoMapper;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Categories.Queries.PagedList;

public class CategoryPagedListQuery() : GetPagedRequestBase, IRequest<ReturnMessage<IPagedList<CategoryResponse>>>
{
    public string Search { get; set; }
}

public class CategoryPagedListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CategoryPagedListQuery, ReturnMessage<IPagedList<CategoryResponse>>>
{
    public async Task<ReturnMessage<IPagedList<CategoryResponse>>> Handle(CategoryPagedListQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<Category, bool>> predicate = null;
        if (!string.IsNullOrEmpty(query.Search))
            predicate = category => category.Name.Contains(query.Search)
                                    || category.Description.Contains(query.Search)
                                    || category.Id.ToString().Contains(query.Search);
        
        var categories = await unitOfWork
            .GetRepository<Category>()
            .GetPagedListAsync(
                selector: category => mapper.Map<CategoryResponse>(category),
                predicate: predicate,
                orderBy: report => report.OrderByDescending(r => r.CreatedAt),
                pageIndex: query.PageIndex,
                pageSize: query.PageSize,
                cancellationToken: cancellationToken);


        if (categories.TotalCount == 0)
            return new ReturnMessage<IPagedList<CategoryResponse>>(data: null, HttpStatusCode.NoContent);

        return new ReturnMessage<IPagedList<CategoryResponse>>(categories, HttpStatusCode.OK);
    }
}
