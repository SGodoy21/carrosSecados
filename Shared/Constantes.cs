using Microsoft.Extensions.Configuration;

namespace Shared;

/// <summary>
/// Static configuration holder and shared constants.
/// </summary>
public static class Constantes
{
    public static IConfiguration oConfig { get; set; } = default!;

    public static string strEvento = "axAnalytics";
}