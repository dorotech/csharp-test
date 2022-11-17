using BookstoreManager.Application.Validator.bookValidator;
using BookstoreManager.Application.Validator.LogErrorValidator;
using BookstoreManager.Domain.dto.ErrorDto;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.LogErrorService.Command.Delete
{
    public class DeleteErrorService : IDeleteErrorService
    {
        private readonly ILogErrorRepository _logErrorRepository;
        private readonly IValidator<CheckhasIdDeleteRequest> _validator;
        public DeleteErrorService(ILogErrorRepository logErrorRepository,
            IValidator<CheckhasIdDeleteRequest> validator)
        {
            _logErrorRepository = logErrorRepository;
            _validator = validator;
        }

        public async Task<DeleteErrorResponse> Delete(int id)
        {

            #region ValidatorRequest
            var validator = _validator.Validate(new CheckhasIdDeleteRequest { Id = id });

            if (!validator.IsValid)
                return  new DeleteErrorResponse(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion 

            var errorLog = await _logErrorRepository.GetByIdAsync(id);
            _logErrorRepository.Delete(errorLog);


            return new DeleteErrorResponse("deletado com sucesso!");

        }
    }
}
