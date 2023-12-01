using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Authors.Commands.Create;

public class CreateAuthorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CreateAuthorCommand, ReturnMessage<AuthorResponse>>
{
    public async Task<ReturnMessage<AuthorResponse>> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Author>();
        var author = await repository
            .GetFirstOrDefaultAsync(author => author.Name.ToLower() == command.Name.ToLower());

        if (author != null)
            return new ReturnMessage<AuthorResponse>(errorMessage: AuthorConstants.AlreadyRegisteredAuthorErrorMessage, HttpStatusCode.Conflict);
        
        author = new Author(command.Name, command.Biography);

        await repository.InsertAsync(author, cancellationToken);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<AuthorResponse>(mapper.Map<AuthorResponse>(author), HttpStatusCode.Created);
    }
}