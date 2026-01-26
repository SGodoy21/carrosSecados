using Application.Interfaces;
using Shared.DTOs.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CrudTableService(ICrudTableRepository crudRepository) : ICrudTableService
    {
        private readonly ICrudTableRepository _crudRepository = crudRepository;
        public async Task<List<CrudConfigDto>> GetAllAsync()
        {
            var filtros = await _crudRepository.GetAllAsync();

            return filtros.Select(f => new CrudConfigDto
            {
                Title = f.Title,
                NombreClave = f.NombreClave,
                ImagenCampo = f.ImagenCampo,
                Crear = f.CrearTexto,
                MostrarEditar = f.MostrarEditar,
                MostrarHabilitar = f.MostrarHabilitar,
                FormRoute = f.FormRoute,       
                Campos = f.CrudsConfigsCampos
                    .OrderBy(c => c.Orden)
                    .Select(c => new CrudConfigCampo
                    {
                        idCampo = c.IdCampo,
                        idCrudConfig = c.IdCrudConfig,
                        nombreCampo = c.NombreCampo,
                        orden = c.Orden,
                        activo = c.Activo
                    }).ToList(),
                Encabezados = f.CrudsConfigsEncabezados
                    .OrderBy(e => e.Orden)
                    .Select(e => new CrudConfigEncabezadosDto
                    {
                        texto = e.Texto,
                       activo = e.Activo,
                        idCrudConfig = e.IdCrudConfig,
                        orden = e.Orden,
                        idEncabezado = e.IdEncabezado
                    }).ToList(),
            }).ToList();
        }

    }
}
