using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICrudTableService
    {
        Task<List<CrudConfigDto>> GetAllAsync();
       // Task<CrudConfigDto> GetByIdAsync(int id);
    }
}
