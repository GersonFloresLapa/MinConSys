using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Request;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class CuentaBancariaService : ICuentaBancariaService
    {
        private readonly ICuentaBancariaRepository _cuentabancariaRepository;


        public CuentaBancariaService(ICuentaBancariaRepository cuentabancariaRepository)
        {
            _cuentabancariaRepository = cuentabancariaRepository;
        }

        public async Task<List<CuentaBancariaDto>> ListarCuentaBancariasAsync()
        {
            var cuentabancarias = await _cuentabancariaRepository.GetAllCuentaBancariasAsync();

            // Mapear a DTO
            var lista = cuentabancarias.Select(p => new CuentaBancariaDto
            {
                IdCuenta    = p.IdCuenta,
                CodigoBanco = p.CodigoBanco,
                TipoCuenta  = p.TipoCuenta,
                NroCuenta   = p.NroCuenta,
                Estado      = p.Estado == 'A' ? "Activo" : "Inactivo"
            }).ToList();

            return lista;
        }

        public async Task<CuentaBancaria> ObtenerPorIdAsync(int id)
        {
            return await _cuentabancariaRepository.GetCuentaBancariaByIdAsync(id);
        }

        public async Task<int> CrearCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria)
        {
            cuentabancaria.FechaCreacion = DateTime.Now;
            cuentabancaria.Estado = "A";
            return await _cuentabancariaRepository.AddCuentaBancariaAsync(cuentabancaria);
        }

        public async Task<bool> ActualizarCuentaBancariaAsync(CuentaBancariaRequest cuentabancaria)
        {
            cuentabancaria.FechaModificacion = DateTime.Now; ;
            return await _cuentabancariaRepository.UpdateCuentaBancariaAsync(cuentabancaria);
        }

        public async Task<bool> EliminarCuentaBancariaAsync(int id, string nombreUsuario)
        {
            return await _cuentabancariaRepository.DeleteCuentaBancariaAsync(id, nombreUsuario);
        }

        public async Task<List<CuentaBancariaDto>> ListarCuentaBancariasByIdEmpresaAsync(int idEmpresa)
        {
      
            var cuentabancarias = await _cuentabancariaRepository.GetCuentaBancariaByIdEmpresaAsync(idEmpresa);

            // Mapear a DTO
            var lista = cuentabancarias.Select(p => new CuentaBancariaDto
            {
                IdCuenta = p.IdCuenta,
                Moneda   = p.Moneda,
                CodigoBanco = p.CodigoBanco,
                TipoCuenta = p.TipoCuenta,
                NroCuenta = p.NroCuenta,
                Estado = p.Estado == 'A' ? "Activo" : "Inactivo"
            }).ToList();

            return lista;


        }

    }
}
