using Domain.Entities;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFiltroRepository
    {
        Task<List<Filtro>> GetAllAsync();
        Task<List<Filtro>> GetByIdAsync(int id);
    }
}
