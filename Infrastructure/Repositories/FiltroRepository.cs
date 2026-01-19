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

        public async Task<List<Filtro>> GetByIdAsync(int Id)
          => await _context.Filtros.AsNoTracking().Where(f => f.IdFiltro == Id).Include(f => f.FiltrosCampos).ToListAsync();
    }
}
