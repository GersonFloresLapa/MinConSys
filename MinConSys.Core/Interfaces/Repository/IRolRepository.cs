using MinConSys.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetAllRolesAsync();
        Task<Rol> GetRolByIdAsync(int id);
        Task<int> AddRolAsync(Rol rol);
        Task<bool> UpdateRolAsync(Rol rol);
        Task<bool> DeleteRolAsync(int id, string usuario);
    }
}
