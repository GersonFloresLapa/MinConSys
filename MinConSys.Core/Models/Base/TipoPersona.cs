using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class TipoPersona
    {
        public int IdTipoPersona { get; set; }
        public string CodigoTipoPersona { get; set; }
        public int IdPersona { get; set; }
        public char Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}