using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<List<EmpresaDto>> ListarEmpresasAsync();
        Task<Empresa> ObtenerPorIdAsync(int id);
        Task<int> CrearEmpresaAsync(Empresa empresa);
        Task<bool> ActualizarEmpresaAsync(Empresa empresa);
        Task<bool> EliminarEmpresaAsync(int id, string usuario);
        Task<List<ComboItem>> ListarEmpresasTiposAsync(string tipo);
    }
}
