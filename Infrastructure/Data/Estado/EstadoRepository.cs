using Application.Interfaces.IEstado;
using Domain.Entities;
using Domain.Enums.Estado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EstadoRepository
{
    public class EstadoRepository(axCarrosManiagroContext context) : IEstadoRepository
    {
        private readonly axCarrosManiagroContext _context = context;
        public async Task<IEnumerable<Estado>> GetByAmbitoAsync(AmbitoEstadoEnum ambito)
        {
            var ambitoStr = ambito.ToString();

            return await _context.Estados
                .AsNoTracking()
                .Where(x => x.Ambito == ambitoStr)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<Estado> GetByIdAsync(long id)
        {
            var estado = await _context.Estados
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (estado == null)
                throw new KeyNotFoundException($"Estado {id} no existe");

            return estado;
        }
    }
}
