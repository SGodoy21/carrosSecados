using Shared;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Application.Interfaces.Negocio
{
    public interface ITiposGraficosHandler
    {
        int TipoGrafico { get; }
        Task<IGraficoResponse> GetDatos(string jsConfig, bool porDefecto);
    }
}