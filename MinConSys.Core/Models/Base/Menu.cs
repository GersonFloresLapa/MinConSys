using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Menu
    {
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string NombreInterno { get; set; }
        public int? PadreId { get; set; }
        public int Orden { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
