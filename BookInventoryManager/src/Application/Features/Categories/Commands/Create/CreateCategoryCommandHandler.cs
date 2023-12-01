using System.Net;
using Application.Common.Constants;
using Application.Common.Interfaces.UnitOfWork;
using Application.Common.Responses;
using AutoMapper;
using CrossCutting.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ReturnMessage<CategoryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ReturnMessage<CategoryResponse>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<Category>();
        var category = await repository
            .GetFirstOrDefaultAsync(category => category.Name.ToLower() == command.Name.ToLower());

        if (category != null)
            return new ReturnMessage<CategoryResponse>(errorMessage: CategoryConstants.AlreadyRegisteredCategoryErrorMessage, HttpStatusCode.Conflict);
        
        category = new Category(command.Name, command.Description);

        await repository.InsertAsync(category, cancellationToken);

        await _unitOfWork.SaveChangesAsync();
        
        return new ReturnMessage<CategoryResponse>(_mapper.Map<CategoryResponse>(category), HttpStatusCode.Created);
    }
}