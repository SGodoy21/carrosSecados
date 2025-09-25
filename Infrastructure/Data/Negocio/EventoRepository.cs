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
    public class EventoRepository(axAnalyticsContext context) : IEventoRepository
    {
        private readonly axAnalyticsContext _context = context;
        public Task<List<Evento>> GetEventosFiltroTiempo(DateTime fechaDesde, DateTime fechaHasta, int idFuenteInicio, int idTipoEventoInicio, int idFuenteFin, int idTipoEventoFin)
        {
            return _context.Eventos.Where(e => e.Fecha >= fechaDesde && e.Fecha <= fechaHasta && ((e.IdFuente == idFuenteInicio && e.IdTipoEvento == idTipoEventoInicio) || (e.IdFuente == idFuenteFin && e.IdTipoEvento == idTipoEventoFin))).ToListAsync();
        }
    }
}
