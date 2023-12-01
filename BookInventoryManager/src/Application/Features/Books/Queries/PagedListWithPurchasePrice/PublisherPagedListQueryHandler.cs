using System.Linq.Expressions;
using System.Net;
using Application.Common.Interfaces;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.Queries.PagedListWithPurchasePrice;

public class BookWithPurchasePricePagedListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<BookWithPurchasePricePagedListQuery, ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>>
{
    public async Task<ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>> Handle(BookWithPurchasePricePagedListQuery query, CancellationToken cancellationToken)
    {
        var publishers = await unitOfWork
            .GetRepository<Book>()
            .GetPagedListAsync(
                selector: category => mapper.Map<BookWithPurchasePriceResponse>(category),
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
            return new ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>(data: null, HttpStatusCode.NoContent);

        return new ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>(publishers, HttpStatusCode.OK);
    }
}