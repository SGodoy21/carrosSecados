using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LoginCleanContext _context;

        public RoleRepository(LoginCleanContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllAsync()
            => await _context.Roles.ToListAsync();

        public async Task<Role> GetByIdAsync(int id)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Role> GetByNameAsync(string name)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);

        public async Task AddAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
