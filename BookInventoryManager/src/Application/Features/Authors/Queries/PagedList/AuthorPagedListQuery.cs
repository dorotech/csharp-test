using Application.Common.Interfaces;
using Application.Common.Requests;
using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Authors.Queries.PagedList;

public class AuthorPagedListQuery() : GetPagedRequestBase, IRequest<ReturnMessage<IPagedList<AuthorResponse>>>
{
    public string Search { get; set; }
}