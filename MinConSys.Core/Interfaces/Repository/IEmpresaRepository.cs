using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetAllEmpresasAsync();
        Task<Empresa> GetEmpresaByIdAsync(int id);
        Task<int> AddEmpresaAsync(Empresa empresa);
        Task<bool> UpdateEmpresaAsync(Empresa empresa);
        Task<bool> DeleteEmpresaAsync(int id, string usuario);
        Task<List<Empresa>> GetEmpresaByTipoAsync(string tipo);
    }
}
