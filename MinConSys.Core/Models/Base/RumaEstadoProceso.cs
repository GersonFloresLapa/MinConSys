using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class RumaEstadoProceso
    {
        public int IdEstadoProceso { get; set; }
        public int IdRuma { get; set; }
        public string EstadoProceso { get; set; }
        public DateTime? FechaCambio { get; set; }
        public string UsuarioCambio { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
