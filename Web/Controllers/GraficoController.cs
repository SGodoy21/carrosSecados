using Application.Services.Graficos;
using Application.Services.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Negocio;
using Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static Shared.DTOs.Negocio.GraficoDto;
using static Shared.DTOs.Negocio.SistemaDto;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Grafico")]
    public class GraficoController(GraficoService graficoService) : Controller
    {
        private readonly GraficoService _graficoService = graficoService;

        [Authorize]
        [HttpGet("GetGraficoBySistema")]
        [SwaggerOperation(Summary = "GetGraficoBySistema", Description = "Obtienen los sistemas asociados al idGrupo del usuario.")]
        [AllowAnonymous]
        public async Task<ActionResult<AxResponse<List<IGraficoResponse>>>> GetGraficoBySistema(int idSistema)
        {
            var graficoResp = await _graficoService.GetGraficoBySistema(idSistema);
            return Ok(AxResponse<List<IGraficoResponse>>.Ok(graficoResp, "Graficos obtenidos correctamente."));
        }
    }
}
