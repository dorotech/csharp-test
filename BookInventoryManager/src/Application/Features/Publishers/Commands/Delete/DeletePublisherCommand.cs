using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Publishers.Commands.Delete;

public class DeletePublisherCommand : IRequest<ReturnMessage>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}