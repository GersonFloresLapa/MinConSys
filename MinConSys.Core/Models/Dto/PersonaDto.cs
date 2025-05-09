using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Dto
{
    public class PersonaDto
    {
        public int IdPersona { get; set; }
        public string DocumentoCompleto { get; set; }  // Tipo + Número
        public string NombreCompleto { get; set; }     // Nombre + Apellidos
        public string Correo { get; set; }
        public string Estado { get; set; }
    }
}
