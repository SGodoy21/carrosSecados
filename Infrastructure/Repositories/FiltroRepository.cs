using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FiltroRepository(axLoginCleanContext context) : IFiltroRepository
    {
        private readonly axLoginCleanContext _context = context;
        public async Task<List<Filtro>> GetAllAsync()
            => await _context.Filtros.AsNoTracking().Include(f => f.FiltrosCampos).ToListAsync();

        public async Task<Filtro?> GetByIdAsync(int Id)
          => await _context.Filtros.Include(f => f.FiltrosCampos).FirstOrDefaultAsync(f => f.IdFiltro == Id);
    }
}
