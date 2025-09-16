using Application.Interfaces.Auth;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Auth
{
    public class UsuarioRepository(axAnalyticsContext context) : IUsuarioRepository
    {
        private readonly axAnalyticsContext _context = context;

        public async Task<Usuario> GetByIdAsync(long usuarioId)
        {
            return await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.Id == usuarioId);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByAliasAsync(string alias)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == alias);
        }

        public async Task<Usuario> GetByAliasAndPasswordAsync(string alias, string password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == alias && u.PasswordHash == password);
        }

        public async Task<Usuario> GetByUsernameAsync(string username)
        {
            return await _context.Usuarios.Include(u => u.Rol).FirstOrDefaultAsync(u => u.NombreUsuario == username);
        }

        public async Task<bool> AddAsync(Usuario newUser)
        {
            _context.Usuarios.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(Usuario user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.Usuarios.Where(u => u.RolId == roleId).ToListAsync();
        }

        public async Task DeleteAsync(Usuario user)
        {
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}