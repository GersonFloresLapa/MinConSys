using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IContratoService
    {
        Task<List<ContratoDto>> ListarContratosAsync();
        Task<Contrato> ObtenerPorIdAsync(int id);
        Task<int> CrearContratoAsync(Contrato contrato);
        Task<bool> ActualizarContratoAsync(Contrato contrato);
        Task<bool> EliminarContratoAsync(int id, string usuario);
    }
}
