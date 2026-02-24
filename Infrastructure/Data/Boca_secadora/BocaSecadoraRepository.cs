using Application.Interfaces.Boca_Secadora;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Boca_secadoraRepository 
{
    internal class BocaSecadoraRepository(axCarrosManiagroContext context) : IBocaSecadoraRepository
    {
        private readonly axCarrosManiagroContext _context = context; 

        public async Task AddAsync(BocaSecadora bocaSecadora)
        {
            await _context.BocasSecadoras.AddAsync(bocaSecadora);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BocaSecadora>> GetAllAsync()
        {
            return await _context.BocasSecadoras
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<BocaSecadora>> GetBocasPorSecadora(long idSecadora)
        {
            return await _context.BocasSecadoras
                 .AsNoTracking()
                 .Where(x => x.IdSecadora == idSecadora)
                 .OrderBy(x => x.Posicion)
                 .ToListAsync();
        }

        public async Task<BocaSecadora> GetByIdAsync(long id)
        {
            var boca = await _context.BocasSecadoras
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (boca == null)
                throw new KeyNotFoundException($"BocaSecadora {id} no existe");

            return boca;
        }

        public async Task UpdateAsync(BocaSecadora bocaSecadora)
        {
            _context.BocasSecadoras.Update(bocaSecadora);
            await _context.SaveChangesAsync();
        }
    }
}
