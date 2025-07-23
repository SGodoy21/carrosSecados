using Application.Services;
using Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        => await _roleService.GetAllAsync();

    [HttpPost("create")]
    public async Task<ActionResult<RoleResponseDto>> Create([FromBody] RoleCreateDto dto)
        => await _roleService.CreateAsync(dto);

    [HttpPut("edit/{id}")]
    public async Task<ActionResult<RoleResponseDto>> Edit(int id, [FromBody] RoleEditDto dto)
    {
        dto.Id = id;
        return await _roleService.EditAsync(dto);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }
}
