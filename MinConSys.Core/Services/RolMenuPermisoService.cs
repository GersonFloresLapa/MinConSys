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
    public class RolMenuPermisoService : IRolMenuPermisoService
    {
        private readonly IRolMenuPermisoRepository _repository;

        public RolMenuPermisoService(IRolMenuPermisoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RolMenuPermiso>> ListarPermisosPorRolAsync(int idRol)
        {
            return await _repository.GetPermisosByRolAsync(idRol);
        }

        public async Task<bool> GuardarPermisosAsync(List<RolMenuPermiso> permisos)
        {
            return await _repository.AddOrUpdatePermisosAsync(permisos);
        }

        public async Task<bool> EliminarPermisosPorRolAsync(int idRol, string usuario)
        {
            return await _repository.DeletePermisosByRolAsync(idRol, usuario);
        }
    }

}
