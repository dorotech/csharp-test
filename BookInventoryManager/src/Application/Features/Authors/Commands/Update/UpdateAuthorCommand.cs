using Application.Common.Responses;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Authors.Commands.Update;

public class UpdateAuthorCommand : IRequest<ReturnMessage<AuthorResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
    
    [FromBody]
    public UpdateAuthorData Data { get; set; }
}