using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IRolMenuPermisoService
    {
        Task<List<RolMenuPermiso>> ListarPermisosPorRolAsync(int idRol);
        Task<bool> GuardarPermisosAsync(List<RolMenuPermiso> permisos);
        Task<bool> EliminarPermisosPorRolAsync(int idRol, string usuario);
    }
}
