using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IVehiculoRepository
    {
        Task<List<Vehiculo>> GetAllVehiculosAsync();
        Task<Vehiculo> GetVehiculoByIdAsync(int id);
        Task<int> AddVehiculoAsync(Vehiculo vehiculo);
        Task<bool> UpdateVehiculoAsync(Vehiculo vehiculo);
        Task<bool> DeleteVehiculoAsync(int id, string usuario);
    }
}
