using Application.Common.Interfaces;
using Application.Common.Requests;
using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Publishers.Queries.PagedList;

public class PublisherPagedListQuery() : GetPagedRequestBase, IRequest<ReturnMessage<IPagedList<PublisherResponse>>>
{
    public string Search { get; set; }
}