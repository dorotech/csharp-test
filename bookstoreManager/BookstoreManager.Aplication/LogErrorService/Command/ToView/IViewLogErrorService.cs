using BookstoreManager.Domain.dto.ErrorDto;

namespace BookstoreManager.Application.LogErrorService.Command.ToView
{
    public interface IViewLogErrorService
    {
        Task<ViewLogErrorResponse> ToView(int id);
    }
}
