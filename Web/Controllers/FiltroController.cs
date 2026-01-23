using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Negocio;
using Shared.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Tags("Filtros")]
public class FiltroController(IFiltroService filtroService) : ControllerBase
{
    private readonly IFiltroService _filtroService = filtroService;

    [HttpGet("list")]
    public async Task<ActionResult<AxResponse<List<FiltroDto>>>> GetFiltros()
    {

        var filtros = await _filtroService.GetAllAsync();
        return Ok(AxResponse<List<FiltroDto>>.Ok(filtros, "filtros obtenidos correctamente."));
    }


    [HttpGet("GetOne")]
    public async Task<ActionResult<AxResponse<FiltroDto>>> GetOneFiltro(int idFiltro)
    {

        var filtro = await _filtroService.GetByIdAsync(idFiltro);
        return Ok(AxResponse<FiltroDto>.Ok(filtro, "filtros obtenidos correctamente."));
    }
}

