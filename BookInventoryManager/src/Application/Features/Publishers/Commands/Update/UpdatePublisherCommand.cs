using Application.Common.Responses;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Publishers.Commands.Update;

public class UpdatePublisherCommand : IRequest<ReturnMessage<PublisherResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
    
    [FromBody]
    public UpdatePublisherData Data { get; set; }
}