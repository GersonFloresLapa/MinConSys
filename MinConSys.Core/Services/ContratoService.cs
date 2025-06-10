using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _contratoRepository;

        public ContratoService(IContratoRepository contratoRepository)
        {
            _contratoRepository = contratoRepository;
        }

        public async Task<List<ContratoDto>> ListarContratosAsync()
        {
            return await _contratoRepository.GetAllContratosAsync();
        }

        public async Task<Contrato> ObtenerPorIdAsync(int id)
        {
            return await _contratoRepository.GetContratoByIdAsync(id);
        }

        public async Task<int> CrearContratoAsync(Contrato contrato)
        {
            contrato.FechaCreacion = DateTime.Now;
            contrato.Estado = "A";
            return await _contratoRepository.AddContratoAsync(contrato);
        }

        public async Task<bool> ActualizarContratoAsync(Contrato contrato)
        {
            contrato.FechaModificacion = DateTime.Now;
            return await _contratoRepository.UpdateContratoAsync(contrato);
        }

        public async Task<bool> EliminarContratoAsync(int id, string usuario)
        {
            return await _contratoRepository.DeleteContratoAsync(id, usuario);
        }
        public async Task<List<ComboItem>> ListarContratosTiposAsync(int? idEmpresa, int? idProveedor)
        {
            var localidades = await _contratoRepository.GetContratoCboAsync(idEmpresa, idProveedor); // Debes implementar esto
            var lista = localidades.Select(e => new ComboItem
            {
                Id = e.IdContrato,
                Descripcion = e.CodigoContrato
            }).ToList();

            return lista;
        }
    }
}
