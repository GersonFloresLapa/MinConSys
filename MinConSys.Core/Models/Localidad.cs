using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoLocalidad { get; set; }
        public string NombreLocalidad { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
