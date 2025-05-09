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
        Task<List<PersonaDto>> GetAllPersonasAsync();
        
    }
}
