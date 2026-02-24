using Application.Interfaces.ICarro;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.CarroRepository
{
    public class CarroRepository(axCarrosManiagroContext context) : ICarroRepository
    {
        private readonly axCarrosManiagroContext _context = context; 
        public async Task AddAsync(Carro carro)
        {
            _context.Carros.Add(carro);
            await _context.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<Carro>> GetAllAsync()
        {
            return await _context.Carros
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Carro> GetByIdAsync(long id)
        {
            return await _context.Carros
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Carro> GetByNumeroCarroAsync(int numCarro)
        {
            return await _context.Carros
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.NumeroCarro == numCarro);
        }

        public async Task<IEnumerable<Carro>> GetCarrosPorLotes(string lote)
        {
            return await _context.Carros
                .AsNoTracking()
                .Where(c => c.Lote == lote)
                .ToListAsync();
        }

        public async Task UpdateAsync(Carro carro)
        {
            _context.Carros.Update(carro);
            await _context.SaveChangesAsync();
        }
    }
}
