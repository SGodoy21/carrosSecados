using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Negocio;
using Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Tags("CrudTable")]
public class CrudTableController(ICrudTableService CrudTableService) : ControllerBase
{

    private readonly ICrudTableService _crudService = CrudTableService;

    [HttpGet("list")]
    public async Task<ActionResult<AxResponse<List<CrudConfigDto>>>> GetCrudTable()
    {

        var filtros = await _crudService.GetAllAsync();
        return Ok(AxResponse<List<CrudConfigDto>>.Ok(filtros, "filtros obtenidos correctamente."));
    }

}

