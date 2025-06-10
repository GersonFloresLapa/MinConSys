using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IVehiculoRepository
    {
        Task<List<VehiculoDto>> GetAllVehiculosAsync();
        Task<Vehiculo> GetVehiculoByIdAsync(int id);
        Task<int> AddVehiculoAsync(Vehiculo vehiculo);
        Task<bool> UpdateVehiculoAsync(Vehiculo vehiculo);
        Task<bool> DeleteVehiculoAsync(int id, string usuario);
        Task<List<Vehiculo>> GetVehiculoByTiposAsync(int idTransportista, string tipo);
    }
}
