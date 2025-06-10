using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IContratoRepository
    {
        Task<List<ContratoDto>> GetAllContratosAsync();
        Task<Contrato> GetContratoByIdAsync(int id);
        Task<int> AddContratoAsync(Contrato contrato);
        Task<bool> UpdateContratoAsync(Contrato contrato);
        Task<bool> DeleteContratoAsync(int id, string usuario);
        Task<List<Contrato>> GetContratoCboAsync(int? idEmpresa, int? idProveedor);
    }
}
