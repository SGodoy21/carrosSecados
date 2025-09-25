using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Negocio
{
    public interface IGraficoRepository
    {
        Task<IReadOnlyList<(int IdTipoGrafico, string JsConfiguracion)>> GetGraficoBySistema(int idSistema);
    }
}
