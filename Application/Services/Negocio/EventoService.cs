using Application.Interfaces.Negocio;
using Domain.Entities;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.DTOs.Negocio.SistemaDto;

namespace Application.Services.Negocio
{
    public class EventoService(IEventoRepository eventoRepository)
    {
        private readonly IEventoRepository _eventoRepository = eventoRepository;

        public async Task<List<Evento>> GetEventosFiltroTiempo(DateTime fechaDesde, DateTime fechaHasta, int idFuenteInicio, int idTipoEventoInicio, int idFuenteFin, int idTipoEventoFin)
        {
            return await eventoRepository.GetEventosFiltroTiempo(fechaDesde, fechaHasta, idFuenteInicio, idTipoEventoInicio, idFuenteFin, idTipoEventoFin);
        }
    }
}
