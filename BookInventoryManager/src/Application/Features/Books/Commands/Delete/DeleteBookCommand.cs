using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Books.Commands.Delete;

public class DeleteBookCommand : IRequest<ReturnMessage>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}