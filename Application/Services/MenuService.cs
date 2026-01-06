using Application.Interfaces;
using Application.Interfaces.Auth;
using Domain.Entities;
using Shared.DTOs.Auth;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MenuService (IMenuRepository menuRepository): IMenuService
    {
        private readonly IMenuRepository _menuRepository = menuRepository;
        public async Task<List<MenuDto>> GetAllAsync()
        {
            var menu = await _menuRepository.GetAllAsync();
            return menu.Select(r => new MenuDto { Id = r.Id, Label = r.Label, Icon= r.Icon, RouterLink=r.RouterLink, Activo=(bool)r.Activo }).ToList();
        }

        public async Task<List<MenuDto>> GetAllActivosAsync()
        {
            var menu = await _menuRepository.GetAllActivosAsync();
            return menu.Select(r => new MenuDto { Id = r.Id, Label = r.Label, Icon = r.Icon, RouterLink = r.RouterLink, Activo = (bool)r.Activo }).ToList();
        }


        public async Task<List<MenuRolDto>> GetAllMenuRolAsync()
        {
            var rol = await _menuRepository.GetAllMenuRolAsync();
            return rol.Select(r => new MenuRolDto { MenuItemId=r.MenuItemId, RolId=r.RolId }).ToList();
        }
    }
}
