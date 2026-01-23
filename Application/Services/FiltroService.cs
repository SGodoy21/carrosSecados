using Application.Interfaces;
using Application.Interfaces.Auth;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FiltroService(IFiltroRepository filtroRepository) : IFiltroService
    {
        private readonly IFiltroRepository _filtroRepository = filtroRepository;


        public async Task<List<FiltroDto>> GetAllAsync()
        {
            var filtros = await _filtroRepository.GetAllAsync();

            return filtros.Select(f => new FiltroDto
            {
                IdFiltro = f.IdFiltro,
                Nombre = f.Nombre,
                ButtonClear = f.ButtonClear,
                Campos = f.FiltrosCampos
                    .OrderBy(c => c.Orden)
                    .Select(c => new FiltroCampoDto
                    {
                        IdCampo = c.IdCampo,
                        Nombre = c.Nombre,
                        Title = c.Title,
                        PlaceHolder = c.PlaceHolder,
                        Format = c.Format,
                        Type = c.Type,
                        Conditions = c.Conditions ?? "",
                        options = c.OptionsSourceCode ?? "",
                        Size = c.Size,
                        Orden = c.Orden
                    }).ToList()
            }).ToList();
        }


        public async Task<FiltroDto?> GetByIdAsync(int Id)
        {
            var filtro = await _filtroRepository.GetByIdAsync(Id);

            return new FiltroDto
            {
                IdFiltro = filtro.IdFiltro,
                Nombre = filtro.Nombre,
                ButtonClear = filtro.ButtonClear,
                Campos = filtro.FiltrosCampos
             .OrderBy(c => c.Orden)
             .Select(c => new FiltroCampoDto
             {
                 IdCampo = c.IdCampo,
                 Nombre = c.Nombre,
                 Title = c.Title,
                 PlaceHolder = c.PlaceHolder,
                 Format = c.Format,
                 Type = c.Type,
                 Conditions = c.Conditions ?? "",
                 options = c.OptionsSourceCode ?? "",
                 Size = c.Size,
                 Orden = c.Orden
             })
             .ToList()
            };
        }


        
    }
}
