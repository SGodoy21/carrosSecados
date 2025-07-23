using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly LoginCleanContext _context;

        public UserRepository(LoginCleanContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(long userId)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByAliasAsync(string alias)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == alias);
        }

        public async Task<User> GetByAliasAndPasswordAsync(string alias, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == alias && u.PasswordHash == password);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> AddAsync(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.Users.Where(u => u.RoleId == roleId).ToListAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}