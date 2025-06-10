using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface IRepresentanteRepository
    {
        Task<List<Representante>> GetAllRepresentantesAsync();
        Task<Representante> GetRepresentanteByIdAsync(int id);
        Task<int> AddRepresentanteAsync(Representante representante);
        Task<bool> UpdateRepresentanteAsync(Representante representante);
        Task<bool> DeleteRepresentanteAsync(int id, string usuario);
        Task<List<RepresentanteDto>> GetRepresentanteByIdEmpresaAsync(int idEmpesa);
    }
}
