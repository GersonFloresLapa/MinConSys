using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinConSys.Core.Models.Base
{
    public class Ley
    {
        public int IdLey { get; set; }
        public int IdRumaEstado { get; set; }
        public int? IdLaboratorio { get; set; }
        public string TipoLey { get; set; }
        public string Producto { get; set; }
        public decimal Contenido { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
