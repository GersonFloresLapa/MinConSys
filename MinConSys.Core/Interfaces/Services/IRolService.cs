using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IRolService
    {
        Task<List<Rol>> ListarRolesAsync();
        Task<Rol> ObtenerPorIdAsync(int id);
        Task<int> CrearRolAsync(Rol rol);
        Task<bool> ActualizarRolAsync(Rol rol);
        Task<bool> EliminarRolAsync(int id, string usuario);
    }
}
