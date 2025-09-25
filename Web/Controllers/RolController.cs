using Application.Exceptions;
using Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs.Auth;
using Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
[Tags("Rol")]
public class RolController(RolService roleService) : ControllerBase
{
    private readonly RolService _roleService = roleService;

    [HttpGet("list")]
    [SwaggerOperation(Summary = "GetAll", Description = "Obtiene todos los roles del sistema.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AxResponse<List<RolResponseDto>>>> GetAll()
    {
        var roles = await _roleService.GetAllAsync();
        return Ok(AxResponse<List<RolResponseDto>>.Ok(roles, "Roles obtenidos correctamente."));
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Create", Description = "Crea un nuevo rol.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<RolResponseDto>>> Create([FromBody] CrearRolDto dto)
    {
        try
        {
            var result = await _roleService.CreateAsync(dto);
            return Ok(AxResponse<RolResponseDto>.Ok(result, "Rol creado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("ya existe"))
                return Conflict(AxResponse<RolResponseDto>.Fail(ex.Message));

            return BadRequest(AxResponse<RolResponseDto>.Fail(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(AxResponse<RolResponseDto>.Fail(ex.Message));
        }
    }

    [HttpPut("edit/{id}")]
    [SwaggerOperation(Summary = "Edit", Description = "Edita un rol existente por su ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<RolResponseDto>>> Edit([FromRoute] int id, [FromBody] EditarRolDto dto)
    {
        dto.Id = id;
        try
        {
            var result = await _roleService.EditAsync(dto);
            return Ok(AxResponse<RolResponseDto>.Ok(result, "Rol actualizado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<RolResponseDto>.Fail(ex.Message));

            if (ex.Message.Contains("ya existe"))
                return Conflict(AxResponse<RolResponseDto>.Fail(ex.Message));

            return BadRequest(AxResponse<RolResponseDto>.Fail(ex.Message));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(AxResponse<RolResponseDto>.Fail(ex.Message));
        }
    }

    [HttpDelete("delete/{id}")]
    [SwaggerOperation(Summary = "Delete", Description = "Elimina un rol por ID. Falla si está asignado a usuarios.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AxResponse<Unit>>> Delete([FromRoute] int id)
    {
        try
        {
            await _roleService.DeleteAsync(id);
            // Usamos 200 + AxResponse<Unit> para mantener el wrapper consistente
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Rol eliminado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("asignado a usuarios"))
                return Conflict(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }
}