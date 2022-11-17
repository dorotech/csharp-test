using BookstoreManager.Domain.dto.ErrorDto;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.LogErrorService.Querie.GetAll
{
    public class GetAllLogErrorService : IGetAllLogErrorService
    {

        private readonly ILogErrorRepository _logErrorRepository;
        public GetAllLogErrorService(ILogErrorRepository logErrorRepository)
        {
            _logErrorRepository = logErrorRepository;
        }

        public async Task<List<GetAllLogErrorResponse>> GetAll(GetAllErrorLogRequest request)
        {

            return await Task.Run(() =>
            {
                var logs = _logErrorRepository.GetAll()
                                          .Where(log => (string.IsNullOrEmpty(request.Search) ||
                                                         log.Message.Contains(request.Search)) &&
                                                         !log.Visualized)
                                          .Select(res => new GetAllLogErrorResponse
                                          {
                                              Id = res.Id,
                                              Message = res.Message,
                                              CreateAt = res.CreateAt,
                                              UpdateAt = res.UpdateAt

                                          }).Skip((request.Page - 1) * request.PageSize)
                                             .Take(request.PageSize).ToList();

                return logs.ToList();
            });
        }
    }
}
