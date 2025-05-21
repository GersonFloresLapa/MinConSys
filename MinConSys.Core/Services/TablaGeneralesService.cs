using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class TablaGeneralesService : ITablaGeneralesService
    {
        private readonly ITablaGeneralesRepository _tablaGeneralesRepository;

        public TablaGeneralesService(ITablaGeneralesRepository tablaGeneralesRepository)
        {
            _tablaGeneralesRepository = tablaGeneralesRepository;
        }

        public async Task<List<TablaGeneralesDto>> ListarTablaGeneralesAsync()
        {
            var tablaGenerales =  await _tablaGeneralesRepository.GetAllTablaGeneralesAsync();

            // Mapear a DTO
            var lista = tablaGenerales.Select(p => new TablaGeneralesDto
            {
                IdGeneral   = p.IdGeneral,
                TipoGeneral = p.TipoGeneral,
                Codigo      = p.Codigo,
                Valor       = p.Valor,
                Descripcion = p.Descripcion,
                Estado = p.Estado == "A" ? "Activo" : "Inactivo"
            }).ToList();

            return lista;
        }

        public async Task<TablaGenerales> ObtenerPorIdAsync(int id)
        {
            return await _tablaGeneralesRepository.GetTablaGeneralesByIdAsync(id);
        }

        public async Task<int> CrearTablaGeneralesAsync(TablaGenerales request)
        {
            request.FechaCreacion = DateTime.Now;
            request.Estado = "A";
            return await _tablaGeneralesRepository.AddTablaGeneralesAsync(request);
        }

        public async Task<bool> ActualizarTablaGeneralesAsync(TablaGenerales request)
        {
            request.FechaModificacion = DateTime.Now;
            return await _tablaGeneralesRepository.UpdateTablaGeneralesAsync(request);
        }
        public async Task<bool> EliminarTablaGeneralesAsync(int id,string nombreUsuario)
        {
            return await _tablaGeneralesRepository.DeleteTablaGeneralesAsync(id, nombreUsuario);
        }
        public async Task<List<TablaGeneralesCombo>> ObtenerPorTipoGeneralAsync(string tipoGeneral)
        {
            return await _tablaGeneralesRepository.GetAllTablaGeneralesByTipoGeneralAsync(tipoGeneral);
        }
        public async Task<List<Ubigeo>> ObtenerUbigeosAsync()
        {
            return await _tablaGeneralesRepository.GetAllUbigeosAsync();
        }
    }
}
