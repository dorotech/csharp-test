using Application.Common.Responses;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommand : IRequest<ReturnMessage>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}