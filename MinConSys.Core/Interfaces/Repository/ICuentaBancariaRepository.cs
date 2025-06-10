using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Repository
{
    public interface ICuentaBancariaRepository
    {
        Task<List<CuentaBancaria>> GetAllCuentaBancariasAsync();
        Task<CuentaBancaria> GetCuentaBancariaByIdAsync(int id);
        Task<int> AddCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria);
        Task<bool> UpdateCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria);
        Task<bool> DeleteCuentaBancariaAsync(int id, string usuario);
        Task<List<CuentaBancaria>> GetCuentaBancariaByIdEmpresaAsync(int idEmpresa);

        
    }
}
