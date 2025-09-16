using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.Auth
{
    public class RolRepository(axAnalyticsContext context) : IRolRepository
    {
        private readonly axAnalyticsContext _context = context;

        public async Task<List<Rol>> GetAllAsync()
            => await _context.Roles.ToListAsync();

        public async Task<Rol> GetByIdAsync(int id)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Rol> GetByNombreAsync(string nombre)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == nombre);

        public async Task AddAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rol rol)
        {
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rol rol)
        {
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }
    }
}