using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface ICuentaBancariaService
    {
        Task<List<CuentaBancariaDto>> ListarCuentaBancariasAsync();
        Task<CuentaBancaria> ObtenerPorIdAsync(int id);
        Task<int> CrearCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria);
        Task<bool> ActualizarCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria);
        Task<bool> EliminarCuentaBancariaAsync(int id, string nombreUsuario);
        Task<List<CuentaBancariaDto>> ListarCuentaBancariasByIdEmpresaAsync(int idEmpresa);

    }
}
