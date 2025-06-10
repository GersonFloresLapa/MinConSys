using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Base;
using MinConSys.Core.Models.Common;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MinConSys.Core.Services
{
    public class ClaseService : IClaseService
    {
        private readonly IClaseRepository _claseRepository;

        public ClaseService(IClaseRepository claseRepository)
        {
            _claseRepository = claseRepository;
        }

        public async Task<List<Clase>> ListarClasesAsync()
        {
            return await _claseRepository.GetAllClasesAsync();
        }

        public async Task<Clase> ObtenerPorIdAsync(int id)
        {
            return await _claseRepository.GetClaseByIdAsync(id);
        }

        public async Task<int> CrearClaseAsync(Clase clase)
        {
            clase.FechaCreacion = DateTime.Now;
            clase.Estado = "A";
            return await _claseRepository.AddClaseAsync(clase);
        }

        public async Task<bool> ActualizarClaseAsync(Clase clase)
        {
            clase.FechaModificacion = DateTime.Now;
            return await _claseRepository.UpdateClaseAsync(clase);
        }

        public async Task<bool> EliminarClaseAsync(int id, string usuario)
        {
            return await _claseRepository.DeleteClaseAsync(id, usuario);
        }

        public async Task<List<ComboItem>> ListarClasesCboAsync()
        {
            var clases = await _claseRepository.GetClaseCboAsync(); // Debes implementar esto
            var lista = clases.Select(e => new ComboItem
            {
                Id = e.IdClase,
                Descripcion = $"{e.Nombre} - {e.Descripcion}"
            }).ToList();

            return lista;
        }


    }
}
