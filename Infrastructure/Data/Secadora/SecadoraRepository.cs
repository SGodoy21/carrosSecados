using Application.Interfaces.ISecadora;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data.SecadoraRepository
{
    public class SecadoraRepository(axCarrosManiagroContext context) : ISecadoraRepository
    {
        private readonly axCarrosManiagroContext _context = context;

        public async Task AddAsync(Secadora secadora)
        {
           _context.Secadoras.Add(secadora);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Secadora>> GetAllAsync()
        {
            return await _context.Secadoras
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Secadora> GetByIdAsync(long id)
        {
            return await _context.Secadoras
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Secadora> GetByNameAsync(string nombre)
        {
            return await _context.Secadoras
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Nombre == nombre);
        }

        public async Task UpdateAsync(Secadora secadora)
        {
            _context.Secadoras.Update(secadora);
            await _context.SaveChangesAsync();

        }
    }
}
