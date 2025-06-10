using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IRepresentanteService
    {
        Task<List<Representante>> ListarRepresentantesAsync();
        Task<Representante> ObtenerPorIdAsync(int id);
        Task<int> CrearRepresentanteAsync(Representante representante);
        Task<bool> ActualizarRepresentanteAsync(Representante representante);
        Task<bool> EliminarRepresentanteAsync(int id, string usuario);
        Task<List<RepresentanteDto>> ListarRepresentantesByIdEmpresaAsync(int idEmpresa);
    }
}
