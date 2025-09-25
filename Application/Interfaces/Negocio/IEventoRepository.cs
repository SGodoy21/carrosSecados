using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Negocio
{
    public interface IEventoRepository
    {
        Task<List<Evento>> GetEventosFiltroTiempo(DateTime fechaDesde, DateTime fechaHasta, int idFuenteInicio, int idTipoEventoInicio, int idFuenteFin, int idTipoEventoFin );
    }
}
