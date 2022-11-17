using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;

namespace BookstoreManager.Application.LogErrorService.Register
{
    public class RegisterLogErrorService : IRegisterLogErrorService
    {
        private readonly ILogErrorRepository _logErrorRepository;
        public RegisterLogErrorService(ILogErrorRepository logErrorRepository)
        {
            _logErrorRepository = logErrorRepository;
        }
        public async Task Register(string Message)
        {
           
            var error = new LogError
            {
                Message = Message,
                Visualized = false
            };

           await _logErrorRepository.Add(error);
        }
    }
}
