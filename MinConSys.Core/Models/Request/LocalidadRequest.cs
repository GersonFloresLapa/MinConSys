using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Request
{
    public class LocalidadRequest
    {
        public int? IdLocalidad { get; set; } // Null cuando sea insertar
        public int IdEmpresa { get; set; }
        public string TipoLocalidad { get; set; }
        public string NombreLocalidad { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public string Estado { get; set; }
    }
}
