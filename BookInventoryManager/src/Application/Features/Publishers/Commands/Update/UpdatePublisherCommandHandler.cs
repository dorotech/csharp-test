using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Publishers.Commands.Update;

public class UpdatePublisherCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdatePublisherCommand, ReturnMessage<PublisherResponse>>
{
    public async Task<ReturnMessage<PublisherResponse>> Handle(UpdatePublisherCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Publisher>();
        
        var publisher = await repository
            .GetFirstOrDefaultAsync(publisher => publisher.Id == command.Id);
        
        if (publisher == null)
            return new ReturnMessage<PublisherResponse>(errorMessage: PublisherConstants.PublisherNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        var publisherByName = await repository
            .GetFirstOrDefaultAsync(publisherDb => publisherDb.Id != command.Id && publisherDb.Name == command.Data.Name);

        if (publisherByName != null)
            return new ReturnMessage<PublisherResponse>(errorMessage: PublisherConstants.AlreadyRegisteredPublisherErrorMessage, HttpStatusCode.Conflict);

        publisher.Update(command.Data.Name);
        
        repository.Update(publisher);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<PublisherResponse>(mapper.Map<PublisherResponse>(publisher), HttpStatusCode.OK);
    }
}