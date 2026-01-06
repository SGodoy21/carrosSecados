using Application.Services;
using Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Auth;
using Shared.DTOs.Negocio;
using Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Auth;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Admin")]
[Tags("Menu")]
public class MenuController(IMenuService menuService) : ControllerBase
{


        private readonly IMenuService _menuService = menuService;

        [HttpGet("list")]
        public async Task<ActionResult<AxResponse<List<MenuDto>>>> GetMenu()
        {

            var menu = await _menuService.GetAllAsync();
            return Ok(AxResponse<List<MenuDto>>.Ok(menu, "menú obtenido correctamente."));
        }

        [HttpGet("listActivos")]
        public async Task<ActionResult<AxResponse<List<MenuDto>>>> GetMenuActivos()
        {

            var menu = await _menuService.GetAllActivosAsync();
            return Ok(AxResponse<List<MenuDto>>.Ok(menu, "menú obtenido correctamente."));
        }

        [HttpGet("GetMenuRoles")]
        public async Task<ActionResult<AxResponse<List<MenuRolDto>>>> GetMenuRoles()
        {

            var rol = await _menuService.GetAllMenuRolAsync();
            return Ok(AxResponse<List<MenuRolDto>>.Ok(rol, "menú obtenido correctamente."));
        }
}

