using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Helpers.TiempoEntreEventos;
using Application.Interfaces.Negocio;
using Application.Services.Negocio;
using Shared;
using Shared.DTOs.Negocio;


namespace Application.Handler
{
    /// <summary>
    /// Handler de gráfico "Tiempos entre eventos".
    /// </summary>
    public class TiemposEntreEventos : ITiposGraficosHandler
    {
        public int TipoGrafico =>  /* tu código/enum correspondiente, por ejemplo: */ 1;

        private readonly EventoService _eventoService;

        public TiemposEntreEventos(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        public async Task<IGraficoResponse> GetDatos(string jsConfigStr, bool porDefecto)
        {
            // 1) Detectamos si el string comienza con comillas dobles escapadas
            if (jsConfigStr.StartsWith("\"") && jsConfigStr.EndsWith("\""))
            {
                // Quitamos el "doble encoding"
                jsConfigStr = JsonSerializer.Deserialize<string>(jsConfigStr);
            }

            var jsConfig = JsonSerializer.Deserialize<JsConfiguracionTiempo>(
                jsConfigStr,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


            if (jsConfig == null)
                return HelperTiempoEntreEventos.GraficoVacio();

            if (porDefecto)
            {
                jsConfig.FechaDesde = DateTime.UtcNow.AddHours(-jsConfig.PlazoHs);
                jsConfig.FechaHasta = DateTime.UtcNow;
            }

            // 2) Obtenemos eventos según el filtro de tiempo
            var eventos = await _eventoService.GetEventosFiltroTiempo(
                jsConfig.FechaDesde, jsConfig.FechaHasta,
                jsConfig.IdFuenteInicio, jsConfig.IdTipoEventoInicio,
                jsConfig.IdFuenteFin, jsConfig.IdTipoEventoFin);

            if (eventos == null || eventos.Count == 0)
                return HelperTiempoEntreEventos.GraficoVacio();

            // 3) Armamos listas para los ejes
            var ejeXDatos = new List<DatoFechaDto>();
            var ejeYDatos = new List<double>();

            var eventosConId = new List<(string CorrelationId, DateTime Fecha, int IdTipoEvento, int IdFuente, string DatoJs)>();

            foreach (var ev in eventos)
            {
                if (!HelperTiempoEntreEventos.TryGetCorrelationId(ev.DatoJs, out var corrId)) continue; // si no hay Id, no podemos emparejar
                eventosConId.Add((corrId, ev.Fecha, ev.IdTipoEvento,ev.IdFuente, ev.DatoJs));
            }

            foreach (var grupo in eventosConId.GroupBy(e => e.CorrelationId))
            {
                var eventosOrdenados = grupo.OrderBy(e => e.Fecha).ToList();

                var pendientes = new Queue<(DateTime Fecha, int IdFuente, string DatoJs)>();

                foreach (var item in eventosOrdenados)
                {

                    // si el evento cumple las condiciones de inicio se almacena
                    if (item.IdTipoEvento == jsConfig.IdTipoEventoInicio && item.IdFuente == jsConfig.IdFuenteInicio)
                    {
                        pendientes.Enqueue((item.Fecha, item.IdFuente, item.DatoJs));
                        continue;
                    }

                    // Es un fin: si tengo un inicio pendiente antes o igual a esta fecha, emparejo
                    if (item.IdTipoEvento == jsConfig.IdTipoEventoFin && item.IdFuente == jsConfig.IdFuenteFin )
                    {
                        while (pendientes.Count > 0)
                        {
                            var inicio = pendientes.Peek();

                            if (item.Fecha >= inicio.Fecha)
                            {
                                pendientes.Dequeue(); // consumir este inicio

                                var minutos = (item.Fecha - inicio.Fecha).TotalMinutes;
                                if (minutos >= 0)
                                {
                                    ejeXDatos.Add(new DatoFechaDto
                                    {
                                        Fecha = inicio.Fecha,
                                        Descripcion = $"{inicio.Fecha:dd/MM/yyyy HH:mm}"
                                    });
                                    ejeYDatos.Add(minutos);
                                }

                                break;
                            }
                            else
                            {
                                //se descarta porque el fin es antes del inicio
                                pendientes.Dequeue();
                            }
                        }
                    }
                }
            }

            // Si no hay datos calculados, devolvemos vacío
            if (ejeYDatos.Count == 0)
                return HelperTiempoEntreEventos.GraficoVacio();

            List<DatoFechaDto> ejeXAgregado = ejeXDatos;
            List<double> ejeYAgregado = ejeYDatos;

            switch (jsConfig.AgrupadoPor)
            {
                case (int)Constantes.IdAgrupamiento.Hora:
                    (ejeXAgregado, ejeYAgregado) = HelperTiempoEntreEventos.AgruparPromedioPorPlazo(
                        ejeXDatos,
                        ejeYDatos,
                        fecha => HelperTiempoEntreEventos.GetPlazoStart(fecha, plazo: Constantes.IdAgrupamiento.Hora)
                    );
                    break;

                case (int)Constantes.IdAgrupamiento.Dia:
                    (ejeXAgregado, ejeYAgregado) = HelperTiempoEntreEventos.AgruparPromedioPorPlazo(
                        ejeXDatos, ejeYDatos,
                        fecha => HelperTiempoEntreEventos.GetPlazoStart(fecha, plazo: Constantes.IdAgrupamiento.Dia)
                    );
                    break;

                case (int)Constantes.IdAgrupamiento.Mes:
                    (ejeXAgregado, ejeYAgregado) = HelperTiempoEntreEventos.AgruparPromedioPorPlazo(
                        ejeXDatos, ejeYDatos,
                        fecha => HelperTiempoEntreEventos.GetPlazoStart(fecha, plazo: Constantes.IdAgrupamiento.Mes)
                    );
                    break;
            }

            ejeXDatos = ejeXAgregado;
            ejeYDatos = ejeYAgregado;
            // 4) Construimos la respuesta fuertemente tipada
            var resp = new GraficoResponseDto<DatoFechaDto, double, DateTime, double>
            {
                Id = 0,
                Nombre = "Tiempos entre eventos",
                ValorMinimo = ejeYDatos.Min(),
                ValorMaximo = ejeYDatos.Max(),
                Promedio = jsConfig.Promedio ? ejeYDatos.Average() : 0,

                EjeX = new GraficoEjeDto<DatoFechaDto, DateTime>
                {
                    Nombre = "Fecha",
                    Unidad = "",
                    Datos = ejeXDatos,
                    ValorMinimo = ejeXDatos.Min(d => d.Fecha),
                    ValorMaximo = ejeXDatos.Max(d => d.Fecha)
                },
                EjeY = new GraficoEjeDto<double, double>
                {
                    Nombre = "Tiempo",
                    Unidad = "Min",
                    Datos = ejeYDatos,
                    ValorMinimo = ejeYDatos.Min(),
                    ValorMaximo = ejeYDatos.Max()
                }
            };

            return resp;


            
        }


        // ---- Helpers locales ----
      
    }
}
