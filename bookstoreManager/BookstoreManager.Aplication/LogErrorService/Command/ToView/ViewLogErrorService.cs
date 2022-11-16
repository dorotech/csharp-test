using BookstoreManager.Domain.dto.ErrorDto;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.LogErrorService.Command.ToView
{
    public class ViewLogErrorService : IViewLogErrorService
    {
        private readonly ILogErrorRepository _logErrorRepository;
        public ViewLogErrorService(ILogErrorRepository logErrorRepository)
        {
            _logErrorRepository = logErrorRepository;
        }
        public async Task<ViewLogErrorResponse> ToView(int id)
        {

            var haslog = await _logErrorRepository.GetById(id);

            haslog.UpdateAt = DateTime.Now;

            await _logErrorRepository.Update(haslog);


            return new ViewLogErrorResponse { Id = haslog.Id, Message = haslog.Message, CreateAt = haslog.CreateAt };

        }
    }
}
