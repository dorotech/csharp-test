using api.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace api.Repositories.user
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;


        public UserRepository(DataContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Converte a senha em um array de bytes.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Converte o array de bytes em uma string hexadecimal.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public async Task<Users> Get(string username, string password)
        {
            _logger.LogInformation($"Getting user: {username}");

            var hashedPassword = HashPassword(password);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == hashedPassword);

            return new Users
            {
                Username = username,
                Password = password,
                Role = user.Role
            };
        }

        public async Task<IEnumerable<Users>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> Get(int Id)
        {
            return await _context.Users.FindAsync(Id);
        }

        public async Task<Users> Post(Users users)
        {
            _logger.LogInformation($"Create User: {users.Username}");

            if (string.IsNullOrWhiteSpace(users.Username) ||
                string.IsNullOrWhiteSpace(users.Password))
            {
                _logger.LogWarning("credentials is not inserted!");
                return null;
            }

            var user = new Users
            {
                Username = users.Username,
                Password = HashPassword(users.Password),
                Role = users.Role
            };

            _logger.LogInformation("creating user");
            _context.Users.Add(user);

            try
            {
                _logger.LogInformation("user created successfully!");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Error register user: {users.Username}");
                return null;
            }

            return user;
        }
    }
}
