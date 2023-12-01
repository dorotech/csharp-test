using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Publishers.Commands.Delete;

public class DeletePublisherCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePublisherCommand, ReturnMessage>
{
    public async Task<ReturnMessage> Handle(DeletePublisherCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Publisher>();
        
        var publisher = await repository
            .GetFirstOrDefaultAsync(publisher => publisher.Id == command.Id);
        
        if (publisher == null)
            return new ReturnMessage(errorMessage: PublisherConstants.PublisherNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        repository.Delete(publisher);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage(HttpStatusCode.NoContent);
    }
}