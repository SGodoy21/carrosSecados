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
    public class GraficoRepository(axAnalyticsContext context) : IGraficoRepository
    {
        private readonly axAnalyticsContext _context = context;

        public async Task<IReadOnlyList<(int IdTipoGrafico, string JsConfiguracion)>> GetGraficoBySistema(int idSistema)
        {
            var rows = await _context.Graficos
                .AsNoTracking()
                .Where(g => g.IdSistema == idSistema)
                .Select(g => new { g.IdTipoGrafico, g.JsConfiguracion })
                .ToListAsync();

            var result = rows
                .Select(x => (x.IdTipoGrafico, x.JsConfiguracion))
                .ToList();

            return result;
        }
    }
}
