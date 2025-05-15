using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<List<Rol>> ListarRolesAsync()
        {
            return await _rolRepository.GetAllRolesAsync();
        }

        public async Task<Rol> ObtenerPorIdAsync(int id)
        {
            return await _rolRepository.GetRolByIdAsync(id);
        }

        public async Task<int> CrearRolAsync(Rol rol)
        {
            rol.Estado = "A";
            rol.FechaCreacion = DateTime.Now;
            return await _rolRepository.AddRolAsync(rol);
        }

        public async Task<bool> ActualizarRolAsync(Rol rol)
        {
            rol.FechaModificacion = DateTime.Now;
            return await _rolRepository.UpdateRolAsync(rol);
        }

        public async Task<bool> EliminarRolAsync(int id, string usuario)
        {
            return await _rolRepository.DeleteRolAsync(id, usuario);
        }
    }
}
