using Application.Common.Responses;
using CrossCutting.Models;

namespace Application.Features.Publishers.Commands.Create;

public record CreatePublisherCommand(string Name, string Biography) : IRequest<ReturnMessage<PublisherResponse>>;