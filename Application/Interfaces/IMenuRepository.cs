using Domain.Entities;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMenuRepository
    {
        Task<List<MenuItem>> GetAllAsync();
        Task<List<MenuItem>> GetAllActivosAsync();
        Task<List<MenuItemRol>> GetAllMenuRolAsync();
    }
}
