using BookManager.Data;
using BookManager.Model;
using BookManager.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BookManager.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        private readonly DataContext _context;
        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}