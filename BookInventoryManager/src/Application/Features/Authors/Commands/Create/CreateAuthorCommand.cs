using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Authors.Commands.Create;

public record CreateAuthorCommand(string Name, string Biography) : IRequest<ReturnMessage<AuthorResponse>>;