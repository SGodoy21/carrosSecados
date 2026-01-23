using Domain.Entities;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFiltroService
    {
        Task<List<FiltroDto>> GetAllAsync();
        Task<FiltroDto> GetByIdAsync(int id);

    }
}
