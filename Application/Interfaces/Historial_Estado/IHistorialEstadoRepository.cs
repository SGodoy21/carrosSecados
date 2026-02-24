using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IHistorial_Estado
{
    public interface IHistorialEstadoRepository
    {
        Task AddAsync(HistorialEstado historialEstado);
        Task UpdateAsync(HistorialEstado historialEstado);
        Task<HistorialEstado?> GetEstadoActualCarroAsync(long carroId);
        Task<HistorialEstado?> GetEstadoActualSecadoraAsync(long secadoraId);
        Task<IEnumerable<HistorialEstado>> GetHistorialCarroAsync(long carroId);
        Task<IEnumerable<HistorialEstado>> GetHistorialSecadoraAsync(long secadoraId); 
        Task<IEnumerable<HistorialEstado>> GetAllAsync(); 
    }
}
