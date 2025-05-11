using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Interfaces.Services
{
    public interface IPersonaService
    {
        Task<List<PersonaDto>> ListarPersonasAsync();
        Task<Persona> ObtenerPorIdAsync(int id);
        Task<int> CrearPersonaAsync(Persona request);
        Task<bool> ActualizarPersonaAsync(Persona request);
        Task<bool> EliminarPersonaAsync(int id, string nombreUsuario);
    }
}
