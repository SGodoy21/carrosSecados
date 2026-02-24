using Application.Interfaces.IHistorial_Estado;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Historial_Estado
{
    public class HistorialEstadoRepository(axCarrosManiagroContext context) : IHistorialEstadoRepository
    {

        private readonly axCarrosManiagroContext _context = context; 

        public async Task AddAsync(HistorialEstado historialEstado)
        {
            await _context.HistorialsEstados.AddAsync(historialEstado);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HistorialEstado>> GetAllAsync()
        {
            return await _context.HistorialsEstados
                .AsNoTracking()
                .Include(x => x.IdEstadoNavigation)
                .OrderByDescending(x => x.FechaHoraInicio)
                .ToListAsync();
        }

        public async Task<HistorialEstado> GetEstadoActualCarroAsync(long carroId)
        {
            var estado = await _context.HistorialsEstados
                .AsNoTracking()
                .Include(x => x.IdEstadoNavigation)
                .FirstOrDefaultAsync(x =>
                    x.IdCarro == carroId &&
                    x.FechaHoraFin == null);

            if (estado == null)
                throw new InvalidOperationException($"Carro {carroId} sin estado activo");

            return estado;
        }

        public async Task<HistorialEstado> GetEstadoActualSecadoraAsync(long secadoraId)
        {
            var estado = await _context.HistorialsEstados
                .AsNoTracking()
                .Include(x => x.IdEstadoNavigation)
                .FirstOrDefaultAsync(x =>
                    x.IdSecadora == secadoraId &&
                    x.FechaHoraFin == null);

            if (estado == null)
                throw new InvalidOperationException($"Secadora {secadoraId} sin estado activo");

            return estado;
        }

        public async Task<IEnumerable<HistorialEstado>> GetHistorialCarroAsync(long carroId)
        {
            return await _context.HistorialsEstados
                .AsNoTracking()
                .Include(x => x.IdEstadoNavigation)
                .Where(x => x.IdCarro == carroId)
                .OrderByDescending(x => x.FechaHoraInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistorialEstado>> GetHistorialSecadoraAsync(long secadoraId)
        {
            return await _context.HistorialsEstados
                .AsNoTracking()
                .Include(x => x.IdEstadoNavigation)
                .Where(x => x.IdSecadora == secadoraId)
                .OrderByDescending(x => x.FechaHoraInicio)
                .ToListAsync();
        }

        public async Task UpdateAsync(HistorialEstado historialEstado)
        {
           _context.HistorialsEstados.Update(historialEstado);
           await _context.SaveChangesAsync(); 
        }
    }
}
