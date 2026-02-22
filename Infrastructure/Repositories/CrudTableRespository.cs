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
    public class CrudTableRespository(axCarrosManiagroContext context) : ICrudTableRepository
    {
        private readonly axCarrosManiagroContext _context = context;
        public async Task<List<CrudConfig>> GetAllAsync()
            => await _context.CrudsConfigs.AsNoTracking().Include(f => f.CrudsConfigsCampos).Include(e => e.CrudsConfigsEncabezados).ToListAsync();
    }
}
