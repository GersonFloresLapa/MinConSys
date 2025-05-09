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
    }
}
