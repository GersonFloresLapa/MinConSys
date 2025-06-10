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
    public interface IPersonaService
    {
        Task<List<PersonaDto>> ListarPersonasAsync();
        Task<Persona> ObtenerPorIdAsync(int id);
        Task<int> CrearPersonaAsync(PersonaRequest persona);
        Task<bool> ActualizarPersonaAsync(PersonaRequest persona);
        Task<bool> EliminarPersonaAsync(int id, string nombreUsuario);
        Task<List<ComboItem>> ListarPersonasTiposAsync(string tipo);
        Task<List<TipoPersona>> ObtenerTipoPersonaPorPersonaAsync(int idEmpresa);

    }
}
