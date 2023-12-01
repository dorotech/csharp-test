using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Publishers.Commands.Create;

public class CreatePublisherCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreatePublisherCommand, ReturnMessage<PublisherResponse>>
{
    public async Task<ReturnMessage<PublisherResponse>> Handle(CreatePublisherCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Publisher>();
        var publisher = await repository
            .GetFirstOrDefaultAsync(publisher => publisher.Name.ToLower() == command.Name.ToLower());

        if (publisher != null)
            return new ReturnMessage<PublisherResponse>(errorMessage: PublisherConstants.AlreadyRegisteredPublisherErrorMessage, HttpStatusCode.Conflict);
        
        publisher = new Publisher(command.Name);

        await repository.InsertAsync(publisher, cancellationToken);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<PublisherResponse>(mapper.Map<PublisherResponse>(publisher), HttpStatusCode.Created);
    }
}