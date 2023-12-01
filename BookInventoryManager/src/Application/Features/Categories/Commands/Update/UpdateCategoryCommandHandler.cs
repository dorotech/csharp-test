using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using AutoMapper;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.Update;

public class UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCategoryCommand, ReturnMessage<CategoryResponse>>
{
    public async Task<ReturnMessage<CategoryResponse>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Category>();
        
        var category = await repository
            .GetFirstOrDefaultAsync(categoryDb => categoryDb.Id == command.Id);
        
        if (category == null)
            return new ReturnMessage<CategoryResponse>(errorMessage: CategoryConstants.CategoryNotRegisteredErrorMessage, HttpStatusCode.NotFound);
        
        var categoryByName = await repository
            .GetFirstOrDefaultAsync(categoryDb => categoryDb.Id != command.Id && categoryDb.Name == command.Data.Name);

        if (categoryByName != null)
            return new ReturnMessage<CategoryResponse>(errorMessage: CategoryConstants.AlreadyRegisteredCategoryErrorMessage, HttpStatusCode.Conflict);

        category.Update(command.Data.Name, command.Data.Description);
        
        repository.Update(category);

        await unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<CategoryResponse>(mapper.Map<CategoryResponse>(category), HttpStatusCode.OK);
    }
}