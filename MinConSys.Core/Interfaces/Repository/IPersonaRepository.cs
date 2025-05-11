using MinConSys.Core.Models;
using MinConSys.Core.Models.Dto;
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
        Task<int> AddPersonaAsync(Persona persona);
        Task<bool> UpdatePersonaAsync(Persona persona);
        Task<bool> DeletePersonaAsync(int id, string usuario);
    }
}
