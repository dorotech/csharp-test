using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Authors.Commands.Update;

public class UpdateAuthorCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<UpdateAuthorCommand, ReturnMessage<AuthorResponse>>
{
    public async Task<ReturnMessage<AuthorResponse>> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Author>();
        
        var author = await repository
            .GetFirstOrDefaultAsync(author => author.Id == command.Id);
        
        if (author == null)
            return new ReturnMessage<AuthorResponse>(errorMessage: AuthorConstants.AuthorNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        var authorByName = await repository
            .GetFirstOrDefaultAsync(authorDb => authorDb.Id != command.Id && authorDb.Name == command.Data.Name);

        if (authorByName != null)
            return new ReturnMessage<AuthorResponse>(errorMessage: AuthorConstants.AlreadyRegisteredAuthorErrorMessage, HttpStatusCode.Conflict);

        author.Update(command.Data.Name, command.Data.Biography);
        
        repository.Update(author);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<AuthorResponse>(mapper.Map<AuthorResponse>(author), HttpStatusCode.OK);
    }
}