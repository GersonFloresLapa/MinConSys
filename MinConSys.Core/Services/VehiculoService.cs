using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoService(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public async Task<List<VehiculoDto>> ListarVehiculosAsync()
        {
            return await _vehiculoRepository.GetAllVehiculosAsync();
        }

        public async Task<Vehiculo> ObtenerPorIdAsync(int id)
        {
            return await _vehiculoRepository.GetVehiculoByIdAsync(id);
        }

        public async Task<int> CrearVehiculoAsync(Vehiculo vehiculo)
        {
            vehiculo.FechaCreacion = DateTime.Now;
            vehiculo.Estado = "A";
            return await _vehiculoRepository.AddVehiculoAsync(vehiculo);
        }

        public async Task<bool> ActualizarVehiculoAsync(Vehiculo vehiculo)
        {
            vehiculo.FechaModificacion = DateTime.Now;
            return await _vehiculoRepository.UpdateVehiculoAsync(vehiculo);
        }

        public async Task<bool> EliminarVehiculoAsync(int id, string usuario)
        {
            return await _vehiculoRepository.DeleteVehiculoAsync(id, usuario);
        }

        public async Task<List<ComboItem>> ListarVehiculosTiposAsync(int idTransportista, string tipo)
        {
            var vehiculos = await _vehiculoRepository.GetVehiculoByTiposAsync(idTransportista, tipo); // Debes implementar esto
            var lista = vehiculos.Select(e => new ComboItem
            {
                Id = e.IdVehiculo,
                Descripcion = e.Placa
            }).ToList();

            return lista;
        }


    }
}
