namespace DoroTech.BookStore.Infrastructure.Persistence;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly BookStoreContext _context;

    public UserRepository(BookStoreContext context) : base(context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByEmail(string email)
        => CurrentSet.FirstOrDefault(x => x.Email == email);

}