using LibraryApp.Data;
using LibraryApp.Dto.User;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class UserRepository
    {
        private readonly LibraryAppContext _context;

        public UserRepository(LibraryAppContext context)
        {
            _context = context;
        }

        public UserTbDto LoginRepo(UserTbDto userDto)
        {
            try
            {
                UserTb user = null;
                user = (from users in _context.UserTbs where users.Email == userDto.Email select users).FirstOrDefault();
                if (user == null)
                    throw new Exception("Invalid Email.");
                else if (!user.Password.Equals(userDto.Password))
                    throw new Exception("Invalid Password.");
                else               
                    return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<UserTbDto> CreateUserRepo(UserTbDto userDto)
        {
            try
            {
                UserTb user = new UserTb
                {
                    Email = userDto.Email,
                    Password = userDto.Password
                };
                _context.UserTbs.Add(user);
                await _context.SaveChangesAsync();

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<UserTb> UpdateUserRepo(UserTb user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<UserTb> DeleteUserRepo(int id)
        {
            try
            {
                var user = (from users in _context.UserTbs select users).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                _context.UserTbs.Remove(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
