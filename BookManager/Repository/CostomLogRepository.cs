using BookManager.Data;
using BookManager.Repository.Interfaces;

namespace BookManager.Repository
{
    public class CostomLogRepository : BaseRepository, ICostomLogRepository
    {
        private readonly DataContext _context;
        public CostomLogRepository(DataContext context) : base(context)
        {
            _context = context;
        }


    }
}