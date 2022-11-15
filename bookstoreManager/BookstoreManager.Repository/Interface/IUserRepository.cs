using BookstoreManager.Domain.Entities;

namespace BookstoreManager.Repository.Interface
{
    public interface IUserRepository 
    {
        Task Add(User user);
        void Delete(User user);
        void Update(User user);
        IEnumerable<User> GetAll(User user);
        Task<User> GetById(int id);
        Task<User> GetByEmail(string email);
    }
}
