using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Auth
{
    public interface IMenuService
    {
        Task<List<MenuDto>> GetAllAsync();
        Task<List<MenuDto>> GetAllActivosAsync();
        Task<List<MenuRolDto>> GetAllMenuRolAsync();
    }
}
