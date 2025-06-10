using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class RumaTicket
    {
        public int IdRumaTicket { get; set; }
        public int IdTicket { get; set; }
        public int IdRuma { get; set; }
        public int IdRumaEstado { get; set; }
        public char Estado { get; set; } = 'A';
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
