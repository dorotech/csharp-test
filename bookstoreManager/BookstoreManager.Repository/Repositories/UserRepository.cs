
using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using BookststorageManager.Data.Data;

namespace BookstoreManager.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }
        public async Task Add(User user)
        {
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
        }

        public void Delete(User user)
        {
            _dataContext.Users.Remove(user);
        }

        public IEnumerable<User> GetAll(User user)
        {
           var result =  _dataContext.Users.AsEnumerable();
            return result;
        }

        public async  Task<User> GetById(int id)
        {
            var result = await _dataContext.Users.FindAsync(id);
            if (result == null)
                return new User() ;
            return result;
        }
        public async Task<User> GetByEmail(string email)
        {
            return await Task.Run(() =>
            {
                var result = _dataContext.Users.FirstOrDefault(e => e.Email.Equals(email));
                if (result == null)
                    return new User();
                return result;
            });
        }
        public void Update(User user)
        {
            _dataContext.Users.Update(user);
        }
    }
}
