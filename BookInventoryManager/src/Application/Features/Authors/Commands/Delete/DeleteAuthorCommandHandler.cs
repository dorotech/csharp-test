using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Authors.Commands.Delete;

public class DeleteAuthorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteAuthorCommand, ReturnMessage>
{
    public async Task<ReturnMessage> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Author>();
        
        var author = await repository
            .GetFirstOrDefaultAsync(author => author.Id == command.Id);
        
        if (author == null)
            return new ReturnMessage(errorMessage: AuthorConstants.AuthorNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        repository.Delete(author);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage(HttpStatusCode.NoContent);
    }
}