using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Authors.Commands.Delete;

public class DeleteAuthorCommand : IRequest<ReturnMessage>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}