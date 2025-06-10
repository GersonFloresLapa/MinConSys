using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class LeyService : ILeyService
    {
        private readonly ILeyRepository _leyRepository;

        public LeyService(ILeyRepository leyRepository)
        {
            _leyRepository = leyRepository;
        }

        public async Task<List<Ley>> ListarLeyesAsync()
        {
            return await _leyRepository.GetAllLeyAsync();
        }

        public async Task<Ley> ObtenerPorIdAsync(int id)
        {
            return await _leyRepository.GetLeyByIdAsync(id);
        }

        public async Task<int> CrearLeyAsync(LeyRequest leyNueva)
        {
            leyNueva.Ley.FechaCreacion = DateTime.Now;
            leyNueva.Ley.Estado = "A";
            return await _leyRepository.AddLeyAsync(leyNueva);
        }

        public async Task<bool> ActualizarLeyAsync(LeyRequest leyActualizar)
        {
            leyActualizar.Ley.FechaModificacion = DateTime.Now;
            return await _leyRepository.UpdateLeyAsync(leyActualizar);
        }

        public async Task<bool> EliminarLeyAsync(int id, string usuario)
        {
            return await _leyRepository.DeleteLeyAsync(id, usuario);
        }
    }
}
