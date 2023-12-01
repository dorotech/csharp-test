using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, ReturnMessage>
{
    public async Task<ReturnMessage> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Category>();
        
        var category = await repository
            .GetFirstOrDefaultAsync(category => category.Id == command.Id);
        
        if (category == null)
            return new ReturnMessage(errorMessage: CategoryConstants.CategoryNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        repository.Delete(category);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage(HttpStatusCode.NoContent);
    }
}