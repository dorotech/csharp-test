using Application.Common.Responses;
using AutoMapper;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommand : IRequest<ReturnMessage<CategoryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
    
    [FromBody]
    public UpdateCategoryData Data { get; set; }
}