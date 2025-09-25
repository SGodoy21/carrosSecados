using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace Shared;

/// <summary>
/// Static configuration holder and shared constants.
/// </summary>
public static class Constantes
{
    public static IConfiguration oConfig { get; set; } = default!;

    public static string strEvento = "axAnalytics";

    public enum IdTipoGraficos
    {
        [Description("Tiempo entre eventos promediado")]
        TiempoEntreEventosPromedio = 1,
    }

    public enum IdAgrupamiento
    {
        Hora = 1,
        Dia = 2,
        Mes = 3,
    }

}