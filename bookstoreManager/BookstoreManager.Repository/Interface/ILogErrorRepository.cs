using BookstoreManager.Domain.Entities;

namespace BookstoreManager.Repository.Interface
{
    public interface ILogErrorRepository
    {
        public Task Add(LogError Error);
        public Task<LogError> GetById(int id);
        public IEnumerable<LogError> GetAll();
        public Task Update(LogError error);
        public void Delete(LogError error);
    }
}
