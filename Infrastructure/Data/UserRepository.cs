using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly LoginCleanContext _context;

    public UserRepository(LoginCleanContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.ToListAsync();

    public async Task<User?> GetByAliasAsync(string alias)
        => await _context.Users.FirstOrDefaultAsync(u => u.Username == alias);

    public async Task<User?> GetByAliasAndPasswordAsync(string alias, string password)
        => await _context.Users.FirstOrDefaultAsync(u => u.Username == alias && u.PasswordHash == password);

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
}
