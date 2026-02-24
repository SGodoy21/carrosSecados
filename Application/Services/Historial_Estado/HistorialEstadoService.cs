using Application.Interfaces.ICarro;
using Application.Interfaces.IHistorial_Estado;
using Application.Interfaces.ISecadora;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Shared.DTOs.Historial_Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Historial_EstadoService
{
    public class HistorialEstadoService(
        IHistorialEstadoRepository historialEstadoRepository,
        ILogger<HistorialEstadoService> logger,
        ISecadoraRepository secadoraRepository,
        ICarroRepository carroRepository)
    {
        private readonly IHistorialEstadoRepository _historialEstado = historialEstadoRepository;
        private readonly ILogger<HistorialEstadoService> _logger = logger;
        private readonly ISecadoraRepository _secadoraRepository = secadoraRepository;
        private readonly ICarroRepository _carroRepository = carroRepository;


        // --- Cambio de Estado del Carro --- // 

        /// <summary>
        /// Registrars the primer estado carro asynchronous.
        /// </summary>
        /// <param name="carroId">The carro identifier.</param>
        /// <param name="estadoId">The estado identifier.</param>
        /// <exception cref="KeyNotFoundException">Carro {carroId} no existe</exception>
        /// <exception cref="InvalidOperationException">El carro {carroId} ya tiene estado inicial</exception>
        public async Task RegistrarPrimerEstadoCarroAsync(long carroId, long estadoId)
        {
            var carro = await _carroRepository.GetByIdAsync(carroId)
                ?? throw new KeyNotFoundException($"Carro {carroId} no existe");

            var actual = await _historialEstado.GetEstadoActualCarroAsync(carro.Id);
            if (actual != null)
                throw new InvalidOperationException($"El carro {carroId} ya tiene estado inicial");

            await _historialEstado.AddAsync(new HistorialEstado
            {
                IdCarro = carroId,
                IdEstado = estadoId,
                FechaHoraInicio = DateTime.UtcNow
            });

            _logger.LogInformation("Primer estado registrado para carro {CarroId}", carroId);
        }

        /// <summary>
        /// Registrars the cambio estado carro asynchronous.
        /// </summary>
        /// <param name="carroId">The carro identifier.</param>
        /// <param name="nuevoEstadoId">The nuevo estado identifier.</param>
        /// <exception cref="InvalidOperationException">Carro {carroId} sin estado activo</exception>
        public async Task RegistrarCambioEstadoCarroAsync(long carroId, long nuevoEstadoId)
        {
            var actual = await _historialEstado.GetEstadoActualCarroAsync(carroId)
                ?? throw new InvalidOperationException($"Carro {carroId} sin estado activo");

            actual.FechaHoraFin = DateTime.UtcNow;
            await _historialEstado.UpdateAsync(actual);

            await _historialEstado.AddAsync(new HistorialEstado
            {
                IdCarro = carroId,
                IdEstado = nuevoEstadoId,
                FechaHoraInicio = DateTime.UtcNow
            });

            _logger.LogInformation(
                "Cambio de estado carro {CarroId}: {EstadoAnterior} -> {EstadoNuevo}",
                carroId,
                actual.IdEstado,
                nuevoEstadoId);
        }


        // --- Cambio de Estado de la Secadora --- // 

        /// <summary>
        /// Registrars the primer estado secadora asynchronous.
        /// </summary>
        /// <param name="secadoraId">The secadora identifier.</param>
        /// <param name="estadoId">The estado identifier.</param>
        /// <exception cref="KeyNotFoundException">Secadora {secadoraId} no existe</exception>
        /// <exception cref="InvalidOperationException">La secadora {secadoraId} ya tiene estado inicial</exception>
        public async Task RegistrarPrimerEstadoSecadoraAsync(long secadoraId, long estadoId)
        {
            var secadora = await _secadoraRepository.GetByIdAsync(secadoraId)
                ?? throw new KeyNotFoundException($"Secadora {secadoraId} no existe");

            var actual = await _historialEstado.GetEstadoActualSecadoraAsync(secadora.Id);
            if (actual != null)
                throw new InvalidOperationException($"La secadora {secadoraId} ya tiene estado inicial");

            await _historialEstado.AddAsync(new HistorialEstado
            {
                IdSecadora = secadoraId,
                IdEstado = estadoId,
                FechaHoraInicio = DateTime.UtcNow
            });

            _logger.LogInformation("Primer estado registrado para secadora {SecadoraId}", secadoraId);
        }

        /// <summary>
        /// Registrars the cambio estado secadora asynchronous.
        /// </summary>
        /// <param name="secadoraId">The secadora identifier.</param>
        /// <param name="nuevoEstadoId">The nuevo estado identifier.</param>
        /// <exception cref="InvalidOperationException">Secadora {secadoraId} sin estado activo</exception>
        public async Task RegistrarCambioEstadoSecadoraAsync(long secadoraId, long nuevoEstadoId)
        {
            var actual = await _historialEstado.GetEstadoActualSecadoraAsync(secadoraId)
                ?? throw new InvalidOperationException($"Secadora {secadoraId} sin estado activo");

            actual.FechaHoraFin = DateTime.UtcNow;
            await _historialEstado.UpdateAsync(actual);

            await _historialEstado.AddAsync(new HistorialEstado
            {
                IdSecadora = secadoraId,
                IdEstado = nuevoEstadoId,
                FechaHoraInicio = DateTime.UtcNow
            });

            _logger.LogInformation(
                "Cambio de estado secadora {SecadoraId}: {Anterior} -> {Nuevo}",
                secadoraId,
                actual.IdEstado,
                nuevoEstadoId);
        }



        // --- Métodos de Historial Base --- // 

        /// <summary>
        /// Gets the historial carro asynchronous.
        /// </summary>
        /// <param name="carroId">The carro identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<HistorialEstadoDto>> GetHistorialCarroAsync(long carroId)
        {
            var historial = await _historialEstado.GetHistorialCarroAsync(carroId);
            return historial.Select(Map);
        }

        /// <summary>
        /// Gets the historial secadora asynchronous.
        /// </summary>
        /// <param name="secadoraId">The secadora identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<HistorialEstadoDto>> GetHistorialSecadoraAsync(long secadoraId)
        {
            var historial = await _historialEstado.GetHistorialSecadoraAsync(secadoraId);
            return historial.Select(Map);
        }

        /// <summary>
        /// Maps the specified h.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <returns></returns>
        private static HistorialEstadoDto Map(HistorialEstado h) => new()
        {
            Estado = h.IdEstadoNavigation?.Nombre ?? "N/A",
            FechaHoraInicio = h.FechaHoraInicio,
            FechaHoraFin = h.FechaHoraFin
        };








    }


}
