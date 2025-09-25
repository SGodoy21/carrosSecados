using Shared.DTOs.Negocio.DatosJsEventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class EventoJsHelper
    {
        public static string GetIdDatoJsEvento(string datoJs)
        {
            if (string.IsNullOrWhiteSpace(datoJs)) return "";

            try
            {
                var v = JsonSerializer.Deserialize<VehiculoJs>(
                    datoJs,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return v?.Id ?? "";
            }
            catch
            {
                return ""; // si el JSON viniera malformado, lo descartamos
            }
        }
    }
}
