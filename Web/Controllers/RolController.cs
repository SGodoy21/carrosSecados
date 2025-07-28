using Application.Services;
using Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Application.Exceptions;
using System;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolController : ControllerBase
{
    private readonly RolService _roleService;

    public RolController(RolService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<RolResponseDto>>> GetAll()
    {
        return await _roleService.GetAllAsync();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CrearRolDto dto)
    {
        try
        {
            var result = await _roleService.CreateAsync(dto);
            return Ok(result);
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("ya existe"))
                return Conflict(new { error = ex.Message });

            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpPut("edit/{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] EditarRolDto dto)
    {
        dto.Id = id;
        try
        {
            var result = await _roleService.EditAsync(dto);
            return Ok(result);
        }
        catch (AppException ex)
        {
            // Opcional: lógica por mensaje si querés diferenciar
            if (ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            if (ex.Message.Contains("ya existe"))
                return Conflict(new { error = ex.Message });

            return BadRequest(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _roleService.DeleteAsync(id);
            return NoContent();
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            if (ex.Message.Contains("asignado a usuarios"))
                return Conflict(new { error = ex.Message });

            return BadRequest(new { error = ex.Message }); // fallback
        }
    }

}
