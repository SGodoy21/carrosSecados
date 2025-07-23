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
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<RoleResponseDto>>> GetAll()
    {
        return await _roleService.GetAllAsync();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] RoleCreateDto dto)
    {
        try
        {
            var result = await _roleService.CreateAsync(dto);
            return Ok(result);
        }
        catch (RoleNameExistsException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] RoleEditDto dto)
    {
        dto.Id = id;
        try
        {
            var result = await _roleService.EditAsync(dto);
            return Ok(result);
        }
        catch (RoleNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (RoleNameExistsException ex)
        {
            return Conflict(new { error = ex.Message });
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
        catch (RoleNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (RoleInUseException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }
}
