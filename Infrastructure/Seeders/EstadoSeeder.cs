using Domain.Entities;
using Domain.Enums.Estado;
using Domain.Seeders.Estado;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeders
{
    public static class EstadoSeeder
    {
        public static async Task SeedAsync(axCarrosManiagroContext context)
        {
            if (context.Estados.Any())
                return;

            var estados = new List<Estado>
        {
            // CARRO
            new() { Id = EstadosIds.Carro_Vacio, Nombre = "Vacío", Ambito = AmbitoEstadoEnum.Carro.ToString() },
            new() { Id = EstadosIds. CargadoParaSecar, Nombre = "Cargado para secar", Ambito = AmbitoEstadoEnum.Carro.ToString() },
            new() { Id = EstadosIds.ListoParaSecar, Nombre = "Listo para secar", Ambito = AmbitoEstadoEnum.Carro.ToString() },
            new() { Id = EstadosIds.Carro_Secando, Nombre = "Secando", Ambito = AmbitoEstadoEnum.Carro.ToString() },
            new() { Id = EstadosIds.SecoEnSecadora, Nombre = "Seco en secadora", Ambito = AmbitoEstadoEnum.Carro.ToString() },
            new() { Id = EstadosIds.SecoParaDescargar, Nombre = "Seco para descargar", Ambito = AmbitoEstadoEnum.Carro.ToString() },

            // SECADORA
            new() { Id = EstadosIds.Secadora_Vacio, Nombre = "Vacío", Ambito = AmbitoEstadoEnum.Secadora.ToString() },
            new() { Id = EstadosIds.CarroListo, Nombre = "Carro listo para secar", Ambito = AmbitoEstadoEnum.Secadora.ToString() },
            new() { Id = EstadosIds.Secadora_Secando, Nombre = "Secando", Ambito = AmbitoEstadoEnum.Secadora.ToString() },
            new() { Id = EstadosIds.Muestrear, Nombre = "Muestrear", Ambito = AmbitoEstadoEnum.Secadora.ToString() },
            new() { Id = EstadosIds.FinSecado, Nombre = "Fin de secado", Ambito = AmbitoEstadoEnum.Secadora.ToString() }
        };

            context.Estados.AddRange(estados);
            await context.SaveChangesAsync();
        }
    }
}
