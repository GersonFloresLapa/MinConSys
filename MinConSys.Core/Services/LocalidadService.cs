using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using MinConSys.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class LocalidadService : ILocalidadService
    {
        private readonly ILocalidadRepository _localidadRepository;

        public LocalidadService(ILocalidadRepository localidadRepository)
        {
            _localidadRepository = localidadRepository;
        }

        public async Task<List<LocalidadDto>> ListarLocalidadesAsync()
        {
 
            return await _localidadRepository.GetAllLocalidadesAsync();

        }

        public async Task<Localidad> ObtenerPorIdAsync(int id)
        {
            return await _localidadRepository.GetLocalidadByIdAsync(id);
        }

        public async Task<int> CrearLocalidadAsync(Localidad request)
        {
            request.FechaCreacion = DateTime.Now;
            return await _localidadRepository.AddLocalidadAsync(request);
        }

        public async Task<bool> ActualizarLocalidadAsync(Localidad request)
        {
            request.FechaModificacion = DateTime.Now;
            return await _localidadRepository.UpdateLocalidadAsync(request);
        }

        public async Task<bool> EliminarLocalidadAsync(int id, string nombreUsuario)
        {
            return await _localidadRepository.DeleteLocalidadAsync(id, nombreUsuario);
        }

        public async Task<List<ComboItem>> ListarLocalidadesTiposAsync(string tipo)
        {
            var localidades = await _localidadRepository.GetLocalidadByTipoAsync(tipo); // Debes implementar esto
            var lista = localidades.Select(e => new ComboItem
            {
                Id = e.IdLocalidad,
                Descripcion = e.NombreLocalidad
            }).ToList();

            return lista;
        }



    }
}
