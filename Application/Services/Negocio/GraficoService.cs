using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.Negocio;
using Shared.DTOs.Negocio;

namespace Application.Services.Graficos
{
    /// <summary>
    /// Servicio que obtiene la lista de gráficos por sistema y delega a los handlers específicos.
    /// </summary>
    public class GraficoService
    {
        private readonly IGraficoRepository _graficoRepository;
        private readonly IDictionary<int, ITiposGraficosHandler> _handlers;

        public GraficoService(
            IGraficoRepository graficoRepository,
            IEnumerable<ITiposGraficosHandler> handlers)
        {
            _graficoRepository = graficoRepository;

            // Indexamos por TipoGrafico para resolver el handler rápidamente
            _handlers = new Dictionary<int, ITiposGraficosHandler>();
            foreach (var h in handlers)
                _handlers[h.TipoGrafico] = h;
        }

        /// <summary>
        /// Devuelve una lista heterogénea de gráficos como IGraficoResponse.
        /// Cada handler devuelve su tipo concreto fuertemente tipado, pero la agregación expone IGraficoResponse.
        /// </summary>
        public async Task<List<IGraficoResponse>> GetGraficoBySistema(int idSistema)
        {
            var listGraficosResp = new List<IGraficoResponse>();
            var graficos = await _graficoRepository.GetGraficoBySistema(idSistema);

            foreach (var grafico in graficos)
            {
                if (!_handlers.TryGetValue(grafico.IdTipoGrafico, out var handler))
                    continue; // opcional: loguear que no se encontró handler

                var resp = await handler.GetDatos(grafico.JsConfiguracion,true);
                if (resp != null)
                    listGraficosResp.Add(resp);
            }

            return listGraficosResp;
        }
    }
    
}
