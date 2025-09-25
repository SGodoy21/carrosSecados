using Application.Services.Auth;
using Application.Services.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Auth;
using Shared.DTOs.Negocio;
using Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Shared.DTOs.Negocio;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Sistema")]
    public class SistemaController(SistemaService sistemaService) : Controller
    {
        private readonly SistemaService _sistemaService = sistemaService;

        [Authorize]
        [HttpPost("GetSistemasByGrupo")]
        [SwaggerOperation(Summary = "GetSistemasByGrupo", Description = "Obtienen los sistemas asociados al idGrupo del usuario.")]
        public async Task<ActionResult<AxResponse<List<SistemaResponseDto>>>> GetSistemasByGrupo()
        {
            var idGrupo = User.FindFirstValue(ClaimTypes.GroupSid) ?? "";

            var id = int.Parse(idGrupo);       

            var sistemas = await _sistemaService.GetByGrupo(id);
            return Ok(AxResponse<List<SistemaResponseDto>>.Ok(sistemas, "Sistemas obtenidos correctamente."));
        }
    }
}
