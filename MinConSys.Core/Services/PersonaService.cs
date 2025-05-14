using MinConSys.Core.Interfaces.Repository;
using MinConSys.Core.Interfaces.Services;
using MinConSys.Core.Models;
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

        public async Task<Persona> ObtenerPorIdAsync(int id)
        {
            return await _personaRepository.GetPersonaByIdAsync(id);
        }

        public async Task<int> CrearPersonaAsync(Persona persona)
        {
            persona.FechaCreacion = DateTime.Now;
            persona.Estado = "A";
            return await _personaRepository.AddPersonaAsync(persona);
        }

        public async Task<bool> ActualizarPersonaAsync(Persona persona)
        {
            persona.FechaModificacion = DateTime.Now;
            return await _personaRepository.UpdatePersonaAsync(persona);
        }

        public async Task<bool> EliminarPersonaAsync(int id,string nombreUsuario)
        {
            return await _personaRepository.DeletePersonaAsync(id, nombreUsuario);
        }
    }
}
