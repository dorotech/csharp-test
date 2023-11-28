using BookstoreManager.Domain.Entities;

namespace BookstoreManager.Repository.Interface
{
    public interface IAuthentication
    {
        public string CreateToken(User users);
    }
}
