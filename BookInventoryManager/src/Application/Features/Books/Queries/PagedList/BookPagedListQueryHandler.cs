using System.Linq.Expressions;
using System.Net;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using Application.Features.Publishers.Queries.PagedList;
using CrossCutting.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.Queries.PagedList;

public class BookPagedListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<BookPagedListQuery, ReturnMessage<IPagedList<BookResponse>>>
{
    public async Task<ReturnMessage<IPagedList<BookResponse>>> Handle(BookPagedListQuery query, CancellationToken cancellationToken)
    {
        var publishers = await unitOfWork
            .GetRepository<Book>()
            .GetPagedListAsync(
                selector: category => mapper.Map<BookResponse>(category),
                predicate: BookPredicate.GetPredicateBySearch(query.Search),
                orderBy: report => report.OrderBy(r => r.Title),
                pageIndex: query.PageIndex,
                pageSize: query.PageSize,
                include: bookQuery => bookQuery
                    .Include(book => book.Category)
                    .Include(book => book.Author)
                    .Include(book => book.Publisher),
                cancellationToken: cancellationToken);

        if (publishers.TotalCount == 0)
            return new ReturnMessage<IPagedList<BookResponse>>(data: null, HttpStatusCode.NoContent);

        return new ReturnMessage<IPagedList<BookResponse>>(publishers, HttpStatusCode.OK);
    }
}