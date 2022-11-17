using BookstoreManager.Domain.dto.ErrorDto;

namespace BookstoreManager.Application.LogErrorService.Querie.GetAll
{
    public interface IGetAllLogErrorService 
    {
        Task<List<GetAllLogErrorResponse>> GetAll(GetAllErrorLogRequest request);
    }
}
