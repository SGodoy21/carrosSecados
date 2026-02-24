using Application.Interfaces.IEstado;
using Domain.Entities;
using Domain.Enums.Estado;
using Microsoft.Extensions.Logging;
using Shared.DTOs.Estado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.EstadoService
{
    internal class EstadoService(
        IEstadoRepository estadoRepository,
        ILogger<EstadoService> logger)
    {
        private readonly ILogger<EstadoService> _logger = logger;
        private readonly IEstadoRepository _estadoRepository = estadoRepository;


        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<EstadoActualDto> GetByIdAsync(long id)
        {
            var estado = await _estadoRepository.GetByIdAsync(id);

            if (estado == null)
            {
                _logger.LogWarning("Estado no encontrado. Id: {Id}", id);
            }

            _logger.LogInformation(
                "Estado obtenido. Id: {Id}, Nombre: {Nombre}, Ambito: {Ambito}",
                estado.Id,
                estado.Nombre,
                estado.Ambito);

            return MapToResponse(estado);
        
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="ambitoEsperado">The ambito esperado.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">El estado {estado.Nombre} no pertenece al ámbito {ambitoEsperado}.</exception>
        public async Task<EstadoActualDto> GetByIdAsync(long id, AmbitoEstadoEnum ambitoEsperado)
        {
            var estado = await _estadoRepository.GetByIdAsync(id);

            var ambitoStr = ambitoEsperado.ToString();

            if (!string.Equals(estado.Ambito, ambitoStr, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogWarning(
                    "Estado con ámbito incorrecto. Id: {Id}, Esperado: {Esperado}, Actual: {Actual}",
                    id, ambitoEsperado, estado.Ambito);

                throw new InvalidOperationException(
                    $"El estado {estado.Nombre} no pertenece al ámbito {ambitoEsperado}.");
            }

            return MapToResponse(estado);
        }

        /// <summary>
        /// Gets the by ambito asynchronous.
        /// </summary>
        /// <param name="ambito">The ambito.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">No hay estados configurados para el ámbito {ambito}.</exception>
        public async Task<IEnumerable<EstadoActualDto>> GetByAmbitoAsync(AmbitoEstadoEnum ambito)
        {
            var estados = (await _estadoRepository.GetByAmbitoAsync(ambito)).ToList();

            if (!estados.Any())
            {
                _logger.LogError("No existen estados configurados para el ámbito {Ambito}", ambito);
                throw new InvalidOperationException(
                    $"No hay estados configurados para el ámbito {ambito}.");
            }

            _logger.LogInformation(
                "Estados consultados por ámbito {Ambito}. Cantidad: {Cantidad}",
                ambito,
                estados.Count);

            return estados.Select(MapToResponse);
        }

        /// <summary>
        /// Maps to response.
        /// </summary>
        /// <param name="estado">The estado.</param>
        /// <returns></returns>
        private EstadoActualDto MapToResponse(Estado estado) => new()
        {
            Id = estado.Id,
            Nombre = estado.Nombre,
            Ambito = estado.Ambito
        };


    }
}