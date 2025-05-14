using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
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
            var localidad = await _localidadRepository.GetAllLocalidadesAsync();

            // Mapear a DTO
            var lista = localidad.Select(p => new LocalidadDto
            {
                IdLocalidad = p.IdLocalidad,
                NombreLocalidad = $"{p.NombreLocalidad}".Trim(),
                Estado = p.Estado == "A" ? "Activo" : "Inactivo"
            }).ToList();

            return lista;
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
    }
}
