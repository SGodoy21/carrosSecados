using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ISecadora_Carro
{
    public interface ISecadoraCarroRepository
    {
        Task AsignarCarroAsync(long secadoraId, long carroId, long usuarioId);
        Task DesasignarCarroAsync(long secadoraId, long carroId);
        Task<SecadoraCarro?> GetAsignacionActivaAsync(long secadoraId);
        Task<IEnumerable<SecadoraCarro>> GetHistorialPorSecadoraAsync(long secadoraId);
        Task<IEnumerable<SecadoraCarro>> GetHistorialPorCarroAsync(long carroId);
    }
}
