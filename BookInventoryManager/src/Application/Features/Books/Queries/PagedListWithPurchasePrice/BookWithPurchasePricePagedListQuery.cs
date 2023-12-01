using Application.Common.Interfaces;
using Application.Common.Requests;
using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Books.Queries.PagedListWithPurchasePrice;

public class BookWithPurchasePricePagedListQuery() : GetPagedRequestBase, IRequest<ReturnMessage<IPagedList<BookWithPurchasePriceResponse>>>
{
    public string Search { get; set; }
}