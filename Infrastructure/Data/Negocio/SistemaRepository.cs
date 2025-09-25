using Application.Interfaces.Negocio;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Negocio
{
    public class SistemaRepository(axAnalyticsContext context) : ISistemaRepository
    {
        private readonly axAnalyticsContext _context = context;

        public async Task<List<Sistema>> GetByGrupo(int idGrupo)
        {
            return await _context.GruposSistemas.Where(s => s.IdGrupo == idGrupo).Include(s => s.IdSistemaNavigation).Select(g => g.IdSistemaNavigation).ToListAsync();
        }

    }
}
