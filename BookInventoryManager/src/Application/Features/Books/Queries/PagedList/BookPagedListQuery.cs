using Application.Common.Interfaces;
using Application.Common.Requests;
using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Books.Queries.PagedList;

public class BookPagedListQuery() : GetPagedRequestBase, IRequest<ReturnMessage<IPagedList<BookResponse>>>
{
    public string Search { get; set; }
}