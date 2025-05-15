using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IRolMenuPermisoRepository
    {
        Task<List<RolMenuPermiso>> GetPermisosByRolAsync(int idRol);
        Task<bool> AddOrUpdatePermisosAsync(List<RolMenuPermiso> permisos);
        Task<bool> DeletePermisosByRolAsync(int idRol, string usuario);
    }
}
