using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models.Dto;
using MinConSys.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<List<PersonaDto>> ListarPersonasAsync()
        {
            var personas =  await _personaRepository.GetAllPersonasAsync();

            // Mapear a DTO
            var lista = personas.Select(p => new PersonaDto
            {
                IdPersona = p.IdPersona,
                DocumentoCompleto = $"{p.TipoDocumento}-{p.NumeroDocumento}",
                NombreCompleto = $"{p.Nombres} {p.ApellidoPaterno} {p.ApellidoMaterno}".Trim(),
                Correo = p.CorreoElectronico,
                Estado = p.Estado == "A" ? "Activo" : "Inactivo"
            }).ToList();

            return lista;
        }
    }
}
