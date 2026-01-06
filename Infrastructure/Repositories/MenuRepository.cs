using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MenuRepository(axLoginCleanContext context) : IMenuRepository
    {
        private readonly axLoginCleanContext _context = context;
        public async Task<List<MenuItem>> GetAllAsync()
           => await _context.MenusItems.ToListAsync();

        public async Task<List<MenuItem>> GetAllActivosAsync()
            => await _context.MenusItems.Where(M=>M.Activo==true).ToListAsync();

        public async Task<List<MenuItemRol>> GetAllMenuRolAsync()
           => await _context.MenusItemsRoles.ToListAsync();
    }
}
