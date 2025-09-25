using Application.Interfaces.Negocio;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.DTOs.Negocio;

namespace Application.Services.Negocio
{
    public class SistemaService(ISistemaRepository sistemaRepository)
    {
        private readonly ISistemaRepository _sistemaRepository = sistemaRepository;

        public async Task<List<SistemaResponseDto>> GetByGrupo(int idGrupo)
        {
            var sistemas = await _sistemaRepository.GetByGrupo(idGrupo);

            var listSistemaDtoResponse = sistemas.Select(s => new SistemaResponseDto { Id = s.Id, Nombre = s.Nombre }).ToList();

            return listSistemaDtoResponse;
        }
    }
}
