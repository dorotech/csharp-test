using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using BookststorageManager.Data.Data;

namespace BookstoreManager.Repository.Repositories
{
    public class LogErrorRepository : ILogErrorRepository
    {
        private readonly DataContext _dataContext;
        public LogErrorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
       
        public async Task Add(LogError Error)
        {
            await _dataContext.AddAsync(Error);
            await _dataContext.SaveChangesAsync();

        }

        public IEnumerable<LogError> GetAll()
        {
            var result = _dataContext.LogErrors.AsEnumerable();
            return result;
        }

        public async Task<LogError> GetById(int id)
        {
            var result = await _dataContext.LogErrors.FindAsync(id);
            if (result == null)
                return new LogError();
            return result;
        }

        public async Task Update(LogError error)
        {
            _dataContext.LogErrors.Update(error);
            await _dataContext.SaveChangesAsync();
        }
        public void Delete(LogError error)
        {
            _dataContext.LogErrors.Remove(error);
           
        }
    }
}
