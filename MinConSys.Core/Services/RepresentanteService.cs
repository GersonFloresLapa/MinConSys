using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class RepresentanteService : IRepresentanteService
    {
        private readonly IRepresentanteRepository _representanteRepository;

        public RepresentanteService(IRepresentanteRepository representanteRepository)
        {
            _representanteRepository = representanteRepository;
        }

        public async Task<List<Representante>> ListarRepresentantesAsync()
        {
            return await _representanteRepository.GetAllRepresentantesAsync();
        }

        public async Task<Representante> ObtenerPorIdAsync(int id)
        {
            return await _representanteRepository.GetRepresentanteByIdAsync(id);
        }

        public async Task<int> CrearRepresentanteAsync(Representante representante)
        {
            representante.Estado = "A";
            representante.FechaCreacion = DateTime.Now;
            return await _representanteRepository.AddRepresentanteAsync(representante);
        }

        public async Task<bool> ActualizarRepresentanteAsync(Representante representante)
        {
            representante.FechaModificacion = DateTime.Now;
            return await _representanteRepository.UpdateRepresentanteAsync(representante);
        }

        public async Task<bool> EliminarRepresentanteAsync(int id, string usuario)
        {
            return await _representanteRepository.DeleteRepresentanteAsync(id, usuario);
        }

        public async Task<List<RepresentanteDto>> ListarRepresentantesByIdEmpresaAsync(int idEmpresa)
        {
            return await _representanteRepository.GetRepresentanteByIdEmpresaAsync(idEmpresa);
        }
    }
}
