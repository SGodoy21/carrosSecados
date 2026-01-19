using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class FiltrosResponseDto
    {
        public List<FiltroDto> Filtros { get; set; } = new();
    }

    public class FiltroDto
    {
        public int IdFiltro { get; set; }
        public string Nombre { get; set; } = "";
        public bool ButtonClear { get; set; }
        public List<FiltroCampoDto> Campos { get; set; } = new();
    }

    public class FiltroCampoDto
    {
        public int IdCampo { get; set; }
        public string Nombre { get; set; } = "";
        public string Title { get; set; } = "";
        public string? PlaceHolder { get; set; }
        public string? Format { get; set; }
        public string Type { get; set; } = "";
        public string? Conditions { get; set; }
        public object? options { get; set; }  // puede ser "" o lista {value,label}
        public string? Size { get; set; }
        public int Orden { get; set; }
    }

    public class OptionItemDto
    {
        public object value { get; set; } = default!;
        public string label { get; set; } = "";
    }
}
