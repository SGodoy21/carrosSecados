using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Seeders.Estado
{
    public static class EstadosIds
    {
        // CARRO (100+)
        public const long Carro_Vacio = 101;
        public const long CargadoParaSecar = 102;
        public const long ListoParaSecar = 103;
        public const long Carro_Secando = 104;
        public const long SecoEnSecadora = 105;
        public const long SecoParaDescargar = 106;

        // SECADORA (200+)
        public const long Secadora_Vacio = 201;
        public const long CarroListo = 202;
        public const long Secadora_Secando = 203;
        public const long Muestrear = 204;
        public const long FinSecado = 205;
    }
}
