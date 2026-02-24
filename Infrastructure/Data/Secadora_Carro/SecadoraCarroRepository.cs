using Application.Interfaces.ISecadora_Carro;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Secadora_CarroRepository
{
    public class SecadoraCarroRepository(axCarrosManiagroContext context) : ISecadoraCarroRepository
    {
        private readonly axCarrosManiagroContext _context = context; 

        public async Task AsignarCarroAsync(long secadoraId, long carroId, long usuarioId)
        {
            var asignacion = new SecadoraCarro
            {
                IdSecadora = secadoraId,
                IdCarro = carroId,
                IdUsuario = usuarioId
            };

            await _context.SecadorasCarros.AddAsync(asignacion);
            await _context.SaveChangesAsync();
        }

        public async Task DesasignarCarroAsync(long secadoraId, long carroId)
        {
            var asignacion = await _context.SecadorasCarros
               .FirstOrDefaultAsync(x =>
                   x.IdSecadora == secadoraId &&
                   x.IdCarro == carroId);

            if (asignacion == null)
                throw new InvalidOperationException("Asignación no encontrada");

            _context.SecadorasCarros.Remove(asignacion);
            await _context.SaveChangesAsync();
        }

        public async Task<SecadoraCarro> GetAsignacionActivaAsync(long secadoraId)
        {
            return await _context.SecadorasCarros
               .AsNoTracking()
               .Where(x => x.IdSecadora == secadoraId)
               .OrderByDescending(x => x.IdCarro) 
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SecadoraCarro>> GetHistorialPorCarroAsync(long carroId)
        {
            return await _context.SecadorasCarros
                .AsNoTracking()
                .Where(x => x.IdCarro == carroId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SecadoraCarro>> GetHistorialPorSecadoraAsync(long secadoraId)
        {
            return await _context.SecadorasCarros
                 .AsNoTracking()
                 .Where(x => x.IdSecadora == secadoraId)
                 .ToListAsync();
        }
    }
}
