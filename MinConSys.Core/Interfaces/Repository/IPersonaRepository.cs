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
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetAllPersonasAsync();
        Task<Persona> GetPersonaByIdAsync(int id);
        Task<int> AddPersonaAsync(PersonaRequest persona);
        Task<bool> UpdatePersonaAsync(PersonaRequest persona);
        Task<bool> DeletePersonaAsync(int id, string usuario);
        Task<List<Persona>> GetPersonasByTipoAsync(string tipo);
        Task<List<TipoPersona>> GetTipoPersonaByPersonaAsync(int idPersona);
    }
}
