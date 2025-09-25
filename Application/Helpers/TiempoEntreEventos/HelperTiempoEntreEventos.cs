using Shared;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Helpers.TiempoEntreEventos
{
    public class HelperTiempoEntreEventos
    {
        /// <summary>
        /// Intenta extraer un Id (case-insensitive) desde un JSON que puede venir doblemente serializado,
        /// con saltos de línea y/o con el Id anidado. Devuelve false si no encontró.
        /// </summary>
        public static bool TryGetCorrelationId(string datoJs, out string id)
        {
            id = null;
            if (string.IsNullOrWhiteSpace(datoJs)) return false;

            // Si viene doblemente serializado (ej: "{\r\n \"id\": \"X\" }"), lo “desempacamos”
            try
            {
                var trimmed = datoJs.Trim();
                if (trimmed.StartsWith("\"") && trimmed.EndsWith("\""))
                {
                    // Esto convierte el string con escapes a JSON crudo
                    datoJs = JsonSerializer.Deserialize<string>(trimmed);
                }
            }
            catch { /* ignorar y seguir con el original */ }

            try
            {
                using var doc = JsonDocument.Parse(datoJs);
                if (TryFindIdRecursive(doc.RootElement, out var found))
                {
                    id = found;
                    return !string.IsNullOrWhiteSpace(id);
                }
            }
            catch
            {
                // Si no es JSON válido, no hay Id
            }
            return false;
        }

        /// <summary>
        /// Busca recursivamente una propiedad llamada "id" (case-insensitive) y devuelve su string.
        /// Si la propiedad no es string/number, la serializa a string.
        /// </summary>
        public static bool TryFindIdRecursive(JsonElement element, out string value)
        {
            value = null;

            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var prop in element.EnumerateObject())
                    {
                        if (prop.NameEquals("id") || prop.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                            return TryGetStringValue(prop.Value, out value);

                        if (TryFindIdRecursive(prop.Value, out value))
                            return true;
                    }
                    return false;

                case JsonValueKind.Array:
                    foreach (var item in element.EnumerateArray())
                    {
                        if (TryFindIdRecursive(item, out value))
                            return true;
                    }
                    return false;

                default:
                    return false;
            }
        }

        public static bool TryGetStringValue(JsonElement el, out string s)
        {
            switch (el.ValueKind)
            {
                case JsonValueKind.String:
                    s = el.GetString();
                    return true;
                case JsonValueKind.Number:
                    s = el.GetRawText(); // número a texto
                    return true;
                case JsonValueKind.True:
                case JsonValueKind.False:
                case JsonValueKind.Null:
                    s = el.GetRawText();
                    return true;
                default:
                    // Para objetos/arrays, lo serializamos (fallback)
                    s = el.GetRawText();
                    return true;
            }
        }

        public static IGraficoResponse GraficoVacio() =>
              new GraficoResponseDto<DatoFechaDto, double, DateTime, double>
              {
                  Id = 0,
                  Nombre = "Tiempos entre eventos",
                  ValorMinimo = 0,
                  ValorMaximo = 0,
                  Promedio = 0,
                  EjeX = new GraficoEjeDto<DatoFechaDto, DateTime>
                  {
                      Nombre = "Fecha",
                      Unidad = "",
                      Datos = new List<DatoFechaDto>(),
                      ValorMinimo = DateTime.MinValue,
                      ValorMaximo = DateTime.MinValue
                  },
                  EjeY = new GraficoEjeDto<double, double>
                  {
                      Nombre = "Tiempo",
                      Unidad = "Min",
                      Datos = new List<double>(),
                      ValorMinimo = 0,
                      ValorMaximo = 0
                  }
              };


        public static (List<DatoFechaDto> ejeX, List<double> ejeY) AgruparPromedioPorPlazo(
            List<DatoFechaDto> ejeXDatos,
            List<double> ejeYDatos,
            Func<DateTime, DateTime> plazoSelector)
        {
            // Seguridad: zip por el mínimo común
            var n = Math.Min(ejeXDatos.Count, ejeYDatos.Count);

            // Acumuladores por plazo (sum, count)
            var acc = new Dictionary<DateTime, (double sum, int count)>();

            for (int i = 0; i < n; i++)
            {
                var fecha = ejeXDatos[i].Fecha;
                var y = ejeYDatos[i];

                var plazo = plazoSelector(fecha);

                if (acc.TryGetValue(plazo, out var sc))
                    acc[plazo] = (sc.sum + y, sc.count + 1);
                else
                    acc[plazo] = (y, 1);
            }

            // Proyección ordenada: X = inicio del plazo, Y = promedio
            var ejeXOut = new List<DatoFechaDto>();
            var ejeYOut = new List<double>();

            foreach (var kv in acc.OrderBy(k => k.Key))
            {
                var plazoFecha = kv.Key;
                var (sum, count) = kv.Value;

                ejeXOut.Add(new DatoFechaDto
                {
                    Fecha = plazoFecha,
                    Descripcion = $"{plazoFecha:dd/MM/yyyy HH}:00" // ej. "25/09/2025 14:00"
                });

                ejeYOut.Add(sum / Math.Max(1, count));
            }

            return (ejeXOut, ejeYOut);
        }

        /// <summary>
        /// Devuelve el inicio del plazo según el tipo de agrupamiento (hora, día, mes).
        /// </summary>
        public static DateTime GetPlazoStart(DateTime fecha, Constantes.IdAgrupamiento plazo)
        {
            // Conservamos el Kind para no perder info (UTC/Local)
            var kind = fecha.Kind;

            switch (plazo)
            {
                case Constantes.IdAgrupamiento.Hora:
                    return new DateTime(fecha.Year, fecha.Month, fecha.Day, fecha.Hour, 0, 0, kind);

                case Constantes.IdAgrupamiento.Dia:
                    return new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0, kind);

                case Constantes.IdAgrupamiento.Mes:
                    return new DateTime(fecha.Year, fecha.Month, 1, 0, 0, 0, kind);

                default:
                    // Sin truncar
                    return fecha;
            }
        }
    }




}