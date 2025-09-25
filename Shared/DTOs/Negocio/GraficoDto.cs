using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Negocio
{
    public class GraficoDto
    {

    }

    public interface IGraficoResponse
    {
        int Id { get; }
        string Nombre { get; }
        object EjeX { get; }
        object EjeY { get; }
        double ValorMinimo { get; }
        double ValorMaximo { get; }
        double Promedio { get; }
    }

    public class GraficoResponseDto<TXData, TYData, TXScalar, TYScalar> : IGraficoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double ValorMinimo { get; set; }
        public double ValorMaximo { get; set; }
        public double Promedio { get; set; }

        public GraficoEjeDto<TXData, TXScalar> EjeX { get; set; }
        public GraficoEjeDto<TYData, TYScalar> EjeY { get; set; }

        object IGraficoResponse.EjeX => EjeX;
        object IGraficoResponse.EjeY => EjeY;
    }

    // Eje con 2 genéricos: datos y tipo de los min/max
    public class GraficoEjeDto<TData, TScalar>
    {
        public string Nombre { get; set; }
        public string Unidad { get; set; }
        public TScalar ValorMinimo { get; set; }
        public TScalar ValorMaximo { get; set; }
        public List<TData> Datos { get; set; } = new();
    }
}
