using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Categories.Commands.Create;

public record CreateCategoryCommand(string Name, string Description) : IRequest<ReturnMessage<CategoryResponse>>;