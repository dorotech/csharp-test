using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Books.Commands.Delete;

public class DeleteBookCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookCommand, ReturnMessage>
{
    public async Task<ReturnMessage> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Book>();
        
        var book = await repository
            .GetFirstOrDefaultAsync(book => book.Id == command.Id);
        
        if (book == null)
            return new ReturnMessage(errorMessage: BookConstants.BookNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        repository.Delete(book);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage(HttpStatusCode.NoContent);
    }
}